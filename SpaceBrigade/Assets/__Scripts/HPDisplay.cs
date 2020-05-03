using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour {
    public GameObject hero;
    private HeroStats stats;
    
    // Use this for initialization
    void Start () {
        stats = hero.gameObject.GetComponent<HeroStateMachine>().hero;
    }
	
	// Update is called once per frame
	void Update () {
        Text buttonText = this.gameObject.GetComponent<Text>();
        buttonText.text = "HP:" + Mathf.CeilToInt(stats.currentHP) + "/" + stats.baseHP;
    }
}
