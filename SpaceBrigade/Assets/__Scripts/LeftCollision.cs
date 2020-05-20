using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCollision : MonoBehaviour
{

    public static LeftCollision S;
    public static Player parentScript;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
            parentScript = this.transform.parent.GetComponent<Player>();
        }
        else Debug.LogError("LeftCollision.Awake() - attempted to assign second LeftCollision.S");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        parentScript.CollideLeft(other);
    }

    private void OnTriggerStay(Collider other)
    {
        parentScript.CollideLeft(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentScript.ReleaseLeft();
    }
}
