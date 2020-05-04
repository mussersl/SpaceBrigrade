using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //states: 0 = ground, 1 = air, 2 = interaction, 3 = menu
    static public Player S;

    [Header("Set Dynamically")]
    float hsp = 0;
    float vsp = 0;
    int state = 0;

    [Header("Set in Inspector")]
    float maxHsp = 2000;
    float maxVsp = 2000;
    float walkSpeed = 400;
    float jump = 1000;
    float grav = -40;

    // Singleton time
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else Debug.LogError("Player.Awake() - attempted to assign second Player.S");
    }

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Vector3 pos = transform.position;
        print("Triggered: " + other.gameObject.name);
        print("State: " + state + ", Vsp: " + vsp);
        GameObject go = other.gameObject;
        if (other.tag == "Collision")
        {
            if (state == 1 && vsp < 0)
            {
                state = 0;
                vsp = 0;
                pos.y = other.transform.position.y + (other.transform.localScale.y / 2) + (transform.localScale.y / 2);
            }
            transform.position = pos;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 pos = transform.position;
        if (other.tag == "Collision")
        {
            pos.y = other.transform.position.y + (other.transform.localScale.y / 2) + (transform.localScale.y / 2);

            /*while (pos.y - 39 <= other.transform.position.y)
            {
                print("here");
                pos.y += 40 * Time.deltaTime;
                //double p = pos.y;
                //pos.y = (float) System.Math.Round(p);
            }*/
            transform.position = pos;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //Vector3 pos = transform.position;
        if (other.tag == "Collision")
        {
            state = 1;
        }
        
    }

    // Update is called once per frame
    void Update () {
        if(this.transform.position.y < 0)
        {
            this.transform.position = new Vector3(513, 269, 0);
        }
        float xAxis = Input.GetAxis("Horizontal");
        hsp = walkSpeed * xAxis;
        if (state == 1)
        {
            if (Input.GetKeyUp(KeyCode.Z) && vsp >= 6)
                vsp = 6;
            vsp += grav;
            if (vsp <= -maxVsp)
                vsp = -maxVsp;
        }
        if (state == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            vsp += jump;
            state = 1;
        }
        Vector3 pos = transform.position;
        pos.x += hsp * Time.deltaTime;
        pos.y += vsp * Time.deltaTime;
        transform.position = pos;
	}
}
