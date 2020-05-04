using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject attackPanel;
    public GameObject enemySelectPanel;
    public bool endCombatCheck;

    // Use this for initialization
    void Start()
    {
        battleState = PerformAction.WAIT;
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Heros.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        HeroInput = HeroGUI.ACTIVATE;

        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(false);

        EnemyButtons();
    }

    // Update is called once per frame
    void Update()
    {
        endCombatCheck = true;
        foreach (GameObject enemy in Enemies)
        {
            if(enemy.GetComponent<EnemyStateMachine>().currentState != EnemyStateMachine.TurnState.DEAD)
            {
                endCombatCheck = false;
            }
        }
        if (endCombatCheck)
        {
            endCombatWin();
        }
        endCombatCheck = true;
        foreach(GameObject hero in Heros)
        {
            if (hero.GetComponent<HeroStateMachine>().currentState != HeroStateMachine.TurnState.DEAD)
            {
                endCombatCheck = false;
            }
        }
        if (endCombatCheck)
        {
            endCombatLoss();
        }
        switch (battleState)
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
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.enemyToAttack = PerformList[0].Defender;
                    HSM.currentState = HeroStateMachine.TurnState.ACTION;
                    battleState = PerformAction.PERFORMACTION;
                }
                break;
            case (PerformAction.PERFORMACTION):
                GameObject person = GameObject.Find(PerformList[0].AttackerName);
                if (PerformList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = person.GetComponent<EnemyStateMachine>();
                    if(ESM.currentState == EnemyStateMachine.TurnState.DEAD)
                    {
                        PerformList.RemoveAt(0);
                        battleState = PerformAction.WAIT;
                    }
                }
                else if (PerformList[0].Type == "Hero")
                {
                    HeroStateMachine HSM = person.GetComponent<HeroStateMachine>();
                    if (HSM.currentState == HeroStateMachine.TurnState.DEAD)
                    {
                        PerformList.RemoveAt(0);
                        battleState = PerformAction.WAIT;
                    }
                }
                break;
        }
        switch(HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if(HerosToManage.Count > 0)
                {
                    HerosToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    HeroTurn = new BattleTurn();

                    attackPanel.SetActive(true);
                    HeroInput = HeroGUI.WAITING;
                }
                break;
            case (HeroGUI.WAITING):
                break;
            case (HeroGUI.DONE):
                HeroInputDone();
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

    public void attackSelect()
    {
        HeroTurn.AttackerName = HerosToManage[0].name;
        HeroTurn.Attacker = HerosToManage[0];
        HeroTurn.Type = "Hero";

        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }
    public void defendSelect()
    {
        HerosToManage[0].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.DEFENDING;
        HerosToManage.RemoveAt(0);
        attackPanel.SetActive(false);
        HeroInput = HeroGUI.ACTIVATE;
    }

    public void enemySelector(GameObject choosenEnemy)
    {
        HeroTurn.Defender = choosenEnemy;
        HeroInput = HeroGUI.DONE;
    }

    void HeroInputDone()
    {
        PerformList.Add(HeroTurn);
        enemySelectPanel.SetActive(false);
        HerosToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        HerosToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    public void endCombatWin()
    {
        SceneManager.LoadScene("Split", LoadSceneMode.Single);
    }
    public void endCombatLoss()
    {
        SceneManager.LoadScene("Split", LoadSceneMode.Single);
    }
}
