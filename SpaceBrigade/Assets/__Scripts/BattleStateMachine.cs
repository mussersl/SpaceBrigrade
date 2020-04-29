using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }
    public PerformAction battleState;

    public List<BattleTurn> PerformList = new List<BattleTurn>();
    public List<GameObject> Heros = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }

    public HeroGUI HeroInput;
    public List<GameObject> HerosToManage = new List<GameObject>();
    private BattleTurn HeroTurn;

    public GameObject enemyButton;
    public Transform enemyButtonSpacer;

    // Use this for initialization
    void Start()
    {
        battleState = PerformAction.WAIT;
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Heros.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        EnemyButtons();
    }

    // Update is called once per frame
    void Update()
    {
        switch(battleState)
        {
            case (PerformAction.WAIT):
                if(PerformList.Count > 0)
                {
                    battleState = PerformAction.TAKEACTION;
                }
                break;
            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].AttackerName);
                if(PerformList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.heroToAttack = PerformList[0].Defender;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                    battleState = PerformAction.PERFORMACTION;
                }
                else if(PerformList[0].Type == "Hero")
                {

                }
                break;
            case (PerformAction.PERFORMACTION):

                break;

        }
    }

    public void GatherActions(BattleTurn input)
    {
        PerformList.Add(input);
    }

    void EnemyButtons()
    {
        foreach(GameObject enemy in Enemies)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            SelectButton button = newButton.GetComponent<SelectButton>();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            buttonText.text = cur_enemy.enemy.name;

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(enemyButtonSpacer,false);
        }
    }
}
