using UnityEngine;
using System.Collections;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    public enum TurnState
    {
        PREPARING,
        CHOOSEACTION,
        IDLE,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for progress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    private Vector3 startposition;
    //For Handling Action
    private bool actionStarted = false;
    public GameObject heroToAttack;
    private float animSpeed = 500f;

    void Start()
    {
        currentState = TurnState.PREPARING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.currentHP <= 0)
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
            case (TurnState.CHOOSEACTION):
                ChooseAction();
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
        }
    }

    void FillProgressBar()
    {
        cur_cooldown += Time.deltaTime;
        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void ChooseAction()
    {
        BattleTurn myAttack = new BattleTurn();
        myAttack.AttackerName = enemy.name;
        myAttack.Attacker = this.gameObject;
        myAttack.Defender = BSM.Heros[Random.Range(0,BSM.Heros.Count)];
        myAttack.Type = "Enemy";
        BSM.GatherActions(myAttack);
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        //animate enemy
        Vector3 heroPosition = new Vector3(heroToAttack.transform.position.x+200f,
                                           heroToAttack.transform.position.y,
                                           heroToAttack.transform.position.z);
        while (MoveTo(heroPosition))
        {
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(0.5f);
        //do damage
        heroToAttack.GetComponent<HeroStateMachine>().hero.Damage(enemy.curATK);

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
        //reset enemy state
        cur_cooldown = 0f;
        currentState = TurnState.PREPARING;
    }

    private bool MoveTo(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, animSpeed * Time.deltaTime));
    }
    
}
