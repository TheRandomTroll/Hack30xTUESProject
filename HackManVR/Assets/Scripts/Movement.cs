using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    public bool rotateBack = true;
    public bool rotateForward = true;
    public bool rotateLeft = true;
    public bool rotateRight = true;

    public bool canMove = true;

    private float speed;

    private int way;


    public Transform playerBlinky;
    public Transform playerPinky;
    public Transform Base;

    public Transform marker;

    public NavMeshAgent navAgent;

    public Transform firstPrsCamera;
    public Vector3 rotation;

    void Start () {
        speed = 8;
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (tag == "Player")
        {
            if (canMove)
            {
                transform.Translate(Vector3.forward / speed);
            }


            if (Input.GetButtonDown("Forward") && rotateForward && way == 3)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);

            }

            if (Input.GetButtonDown("Backward") && rotateBack && way != 3)
            {

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);


                way = 3;
            }

            if (Input.GetButtonDown("Left") && rotateLeft)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);



                way = 4;

            }

            if (Input.GetButtonDown("Right") && rotateRight)
            { 
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);


                way = 2;
            }

            
        }


        if (this.tag == "Ghost")
        {
            if (this.name == "Blinky")
            {
                if (Time.timeSinceLevelLoad > 3)
                {
                    navAgent.SetDestination(playerBlinky.position);
                }
            }

            if (this.name == "Pinky")
            {
                if (Time.timeSinceLevelLoad > 10)
                {
                    navAgent.SetDestination(playerPinky.position);
                }

            }

            if (this.name == "Inky")
            {
                if (Time.timeSinceLevelLoad > 15)
                {
                    if (Vector3.Distance(transform.position, playerBlinky.position) < 3)
                    {
                        navAgent.SetDestination(Base.position);
                    }
                    else
                    {
                        navAgent.SetDestination(playerBlinky.position);
                    }
                }
            }

            if (this.name == "Clyde")
            {
                if (Time.timeSinceLevelLoad > 25)
                {
                    if (Vector3.Distance(transform.position, playerBlinky.position) < 3)
                    {
                        navAgent.SetDestination(marker.position);
                    }
                    else
                    {
                        navAgent.SetDestination(playerBlinky.position);
                    }
                }
            }
        }

    }    
}
