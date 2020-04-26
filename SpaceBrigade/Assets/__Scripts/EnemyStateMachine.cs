using UnityEngine;
using System.Collections;

public class EnemyStateMachine : MonoBehaviour
{
    public BaseEnemy enemy;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PREPARING):
                FillProgressBar();
                break;
            case (TurnState.ADDTOLIST):

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
        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
}
