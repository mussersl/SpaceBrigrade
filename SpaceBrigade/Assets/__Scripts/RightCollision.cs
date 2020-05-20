using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCollision : MonoBehaviour
{

    public static RightCollision S;
    public static Player parentScript;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
            parentScript = this.transform.parent.GetComponent<Player>();
        }
        else Debug.LogError("RightCollision.Awake() - attempted to assign second RightCollision.S");
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
        parentScript.CollideRight(other);
    }

    private void OnTriggerStay(Collider other)
    {
        parentScript.CollideRight(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentScript.ReleaseRight();
    }
}
