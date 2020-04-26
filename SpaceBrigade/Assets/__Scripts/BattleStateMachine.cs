using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }
    public PerformAction battleState;

    public List<BattleTurns> PerfromList = new List<BattleTurns>();
    public List<GameObject> Heros = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        battleState = PerformAction.WAIT;
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Heros.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
    }

    // Update is called once per frame
    void Update()
    {
        switch(battleState)
        {
            case (PerformAction.WAIT):

                break;
            case (PerformAction.TAKEACTION):

                break;
            case (PerformAction.PERFORMACTION):

                break;

        }
    }
}
