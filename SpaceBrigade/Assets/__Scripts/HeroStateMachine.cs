using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    public HeroStats hero;
    private BattleStateMachine BSM;

    public enum TurnState
    {
        PREPARING,
        ADDTOLIST,
        IDLE,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    //for prgress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    public Image ProgressBar;

    // Use this for initialization
    void Start()
    {
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PREPARING;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case (TurnState.PREPARING):
                FillProgressBar();
                break;
            case (TurnState.ADDTOLIST):
                BSM.HerosToManage.Add(this.gameObject);
                currentState = TurnState.IDLE;
                break;
            case (TurnState.IDLE):

                break;
            case (TurnState.ACTION):

                break;
            case (TurnState.DEAD):

                break;
        }
    }

    void FillProgressBar()
    {
        cur_cooldown += Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = 
            new Vector3(Mathf.Clamp(calc_cooldown,0,1)
                ,ProgressBar.transform.localScale.y
                ,ProgressBar.transform.localScale.z);
        if(cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
}
