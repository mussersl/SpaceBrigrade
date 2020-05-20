using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //states: 0 = ground, 1 = air, 2 = interaction, 3 = menu
    static public Player S;
    static public GameObject BottomCollision;
    static public GameObject TopCollision;
    static public GameObject RightCollision;
    static public GameObject LeftCollision;
    Collider ColliderR;
    Collider ColliderL;
    Collider ColliderT;
    Collider ColliderB;

    [Header("Set Dynamically")]
    float hsp = 0;
    float vsp = 0;
    int state = 0;
    bool top = false;
    bool bottom = true;
    bool left = false;
    bool right = false;

    [Header("Set in Inspector")]
    float maxHsp = 2000;
    float maxVsp = 2000;
    float walkSpeed = 400;
    float jump = 1000;
    float grav = -40;

    public GameObject playerImage;

    //for falling off stage
    Vector3 startPosition;

    // Singleton time
    private void Awake()
    {
        if (S == null)
        {
            S = this;
            BottomCollision = S.transform.GetChild(0).gameObject;
            RightCollision = S.transform.GetChild(1).gameObject;
            TopCollision = S.transform.GetChild(2).gameObject;
            LeftCollision = S.transform.GetChild(3).gameObject;
            ColliderR = null;
            ColliderL = null;
            ColliderT = null;
            ColliderB = null;
        }
        else Debug.LogError("Player.Awake() - attempted to assign second Player.S");
    }

    // Use this for initialization
    void Start () {
        startPosition = this.transform.position;
	}

    public void CollideBottom(Collider other)
    {
        bottom = true;
        ColliderB = other;
    }

    public void ReleaseBottom()
    {
        bottom = false;
        ColliderB = null;
    }

    public void CollideRight(Collider other)
    {
        //Vector3 pos = transform.position;

        //transform.position = pos;

        right = true;
        ColliderR = other;
    }

    public void ReleaseRight()
    {
        right = false;
        ColliderR = null;
    }

    public void CollideTop(Collider other)
    {
        //Vector3 pos = transform.position;

        //transform.position = pos;

        top = true;
        ColliderT = other;
    }

    public void ReleaseTop()
    {
        top = false;
        ColliderT = null;
    }

    public void CollideLeft(Collider other)
    {
        //Vector3 pos = transform.position;

        //transform.position = pos;

        left = true;
        ColliderL = other;
    }

    public void ReleaseLeft()
    {
        left = false;
        ColliderL = null;
    }

    public void Collide()
    {
        Vector3 pos = transform.position;
        if (bottom == true)
        {
            if (ColliderB.tag == "Collision")
            {
                if (state == 1 && vsp < 0)
                {
                    state = 0;
                    vsp = 0;
                    pos.y = ColliderB.transform.position.y + (ColliderB.transform.localScale.y / 2) + (transform.localScale.y / 2);
                }
            }
        }
        if (right == true)
        {
            if (ColliderR.tag == "Collision")
            {
                print(ColliderR);
                if (hsp > 0)
                {
                    hsp = 0;
                    pos.x = ColliderR.transform.position.x - (ColliderR.transform.localScale.x / 2) - (transform.localScale.x / 2);
                }
            }
        }
        if (top == true)
        {
            if (ColliderT.tag == "Collision")
            {
                if (state == 1 && vsp > 0)
                {
                    vsp = 0;
                    pos.y = ColliderT.transform.position.y - (ColliderT.transform.localScale.y / 2) - (transform.localScale.y / 2);
                }
            }
        }
        if (left == true)
        {
            if (ColliderL.tag == "Collision")
            {
                if (hsp < 0)
                {
                    hsp = 0;
                    pos.x = ColliderL.transform.position.x + (ColliderL.transform.localScale.x / 2) + (transform.localScale.x / 2);
                }
            }
        }
        transform.position = pos;
    }
    /*private void OnTriggerEnter(Collider other)
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
        
    }*/

    /*private void OnTriggerStay(Collider other)
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
            }
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
        
    }*/

    // Update is called once per frame
    void Update () {
        if(this.transform.position.y < -100)
        {
            this.transform.position = startPosition;
        }
        float xAxis = Input.GetAxis("Horizontal");
        hsp = walkSpeed * xAxis;
        if (hsp < 0)
        {
            playerImage.GetComponent<SpriteRenderer>().flipX = true;
        } else if(hsp > 0)
        {
            playerImage.GetComponent<SpriteRenderer>().flipX = false;
        }
        Collide();
        if (state == 0 && bottom == false)
        {
            state = 1;
        }
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
        if (state == 1)
            pos.y += vsp * Time.deltaTime;
        transform.position = pos;
	}
}
