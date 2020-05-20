using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCollision : MonoBehaviour
{

    public static TopCollision S;
    public static Player parentScript;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
            parentScript = this.transform.parent.GetComponent<Player>();
        }
        else Debug.LogError("TopCollision.Awake() - attempted to assign second TopCollision.S");
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
        parentScript.CollideTop(other);
    }

    private void OnTriggerStay(Collider other)
    {
        parentScript.CollideTop(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentScript.ReleaseTop();
    }
}
