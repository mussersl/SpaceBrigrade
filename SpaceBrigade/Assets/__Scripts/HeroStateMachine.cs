using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    public HeroStats hero;
    private BattleStateMachine BSM;
    public GameObject selector;

    public enum TurnState
    {
        PREPARING,
        ADDTOLIST,
        IDLE,
        ACTION,
        DEAD,
        DEFENDING
    }

    public TurnState currentState;
    //for prgress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image ProgressBar;

    //animation
    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startposition;
    private float animSpeed = 500f;

    // Use this for initialization
    void Start()
    {
        resetCooldown();
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PREPARING;
        selector.SetActive(false);
        actionStarted = false;
        startposition = transform.position;
    }

    private void resetCooldown()
    {
        cur_cooldown = Random.Range(0, max_cooldown / 10) + (max_cooldown / 100 * hero.dexterity);
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.currentHP <= 0)
        {
            currentState = TurnState.DEAD;
        }
        switch (currentState)
        {
            case (TurnState.PREPARING):
                if (!BSM.pause)
                {
                    FillProgressBar();
                }
                break;
            case (TurnState.ADDTOLIST):
                BSM.HerosToManage.Add(this.gameObject);
                currentState = TurnState.IDLE;
                break;
            case (TurnState.IDLE):

                break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):
                this.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                break;
            case (TurnState.DEFENDING):
                hero.defending = true;
                resetCooldown();
                currentState = TurnState.PREPARING;
                break;
        }
    }

    void FillProgressBar()
    {
        cur_cooldown += Time.deltaTime;
        SetProgressBar();
    }

    void SetProgressBar()
    {
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale =
            new Vector3(Mathf.Clamp(calc_cooldown, 0, 1)
                , ProgressBar.transform.localScale.y
                , ProgressBar.transform.localScale.z);
        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        hero.defending = false;
        //animate hero
        Vector3 enemyPosition = new Vector3(enemyToAttack.transform.position.x - 200f,
                                           enemyToAttack.transform.position.y,
                                           enemyToAttack.transform.position.z);
        while (MoveTo(enemyPosition))
        {
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(0.5f);
        //do damage
        enemyToAttack.GetComponent<EnemyStateMachine>().enemy.Damage(hero.getAttack());

        //animate back to start
        while (MoveTo(startposition))
        {
            yield return null;
        }

        //remove action from BattleStateMachine
        BSM.PerformList.RemoveAt(0);
        //reset BattleStateMachine
        BSM.battleState = BattleStateMachine.PerformAction.WAIT;
        //end coroutine
        actionStarted = false;
        //reset hero state
        resetCooldown();
        SetProgressBar();
        currentState = TurnState.PREPARING;
    }

    private bool MoveTo(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, animSpeed * Time.deltaTime));
    }
}
