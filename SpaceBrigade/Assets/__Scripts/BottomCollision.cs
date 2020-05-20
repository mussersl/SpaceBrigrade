using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCollision : MonoBehaviour {

    public static BottomCollision S;
    public static Player parentScript;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
            parentScript = this.transform.parent.GetComponent<Player>();
        }
        else Debug.LogError("BottomCollision.Awake() - attempted to assign second BottomCollision.S");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        parentScript.CollideBottom(other);
    }

    private void OnTriggerStay(Collider other)
    {
        parentScript.CollideBottom(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentScript.ReleaseBottom();
    }
}
