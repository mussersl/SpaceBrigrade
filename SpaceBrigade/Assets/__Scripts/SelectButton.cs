using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {
    public GameObject EnemyPrefab;

    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().enemySelector(EnemyPrefab);
    }

    private void Update()
    {
        if(EnemyPrefab.GetComponent<EnemyStateMachine>().enemy.currentHP < 0)
        {
            this.GetComponent<Button>().interactable = false;
        }
    }
}
