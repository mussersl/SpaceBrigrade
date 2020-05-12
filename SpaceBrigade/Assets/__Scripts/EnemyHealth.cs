using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public Image enemyHP;
    public GameObject enemy;
    private BaseEnemy enemyStats;

	// Use this for initialization
	void Start () {
        enemyStats = enemy.GetComponent<EnemyStateMachine>().enemy;
	}
	
	// Update is called once per frame
	void Update () {
        float currentHP = enemyStats.currentHP / enemyStats.baseHP;
        enemyHP.transform.localScale =
            new Vector3(Mathf.Clamp(currentHP, 0, 1)
                , enemyHP.transform.localScale.y
                , enemyHP.transform.localScale.z);
    }
}
