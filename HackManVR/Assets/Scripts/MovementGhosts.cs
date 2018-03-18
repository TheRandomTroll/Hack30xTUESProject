using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementGhosts : MonoBehaviour {

    public bool [] bigPointMarkersChecker = new bool[4];
    public Transform[] bigPointMarker = new Transform[4];

    public Transform playerBlinky;
    public Transform playerPinky;
    public Transform Base;
    public Transform marker;

    public NavMeshAgent navAgent;

    public bool bigPointEaten = false;

    void Start () {

        bigPointMarker[0] = GameObject.FindGameObjectWithTag("Marker 1").GetComponent<Transform>();
        bigPointMarker[1] = GameObject.FindGameObjectWithTag("Marker 2").GetComponent<Transform>();
        bigPointMarker[2] = GameObject.FindGameObjectWithTag("Marker 3").GetComponent<Transform>();
        bigPointMarker[3] = GameObject.FindGameObjectWithTag("Marker 4").GetComponent<Transform>();


        playerBlinky = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPinky = GameObject.FindGameObjectWithTag("detectorPinky").GetComponent<Transform>();
        Base = GameObject.FindGameObjectWithTag("Base").GetComponent<Transform>();
        marker = GameObject.FindGameObjectWithTag("Marker 1").GetComponent<Transform>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {


        if (this.name == "Blinky")
        {
            if (Time.timeSinceLevelLoad > 3)
            {
                navAgent.SetDestination(playerBlinky.position);
                StartCoroutine(TimerBigPoint());
            }

            if (Time.timeSinceLevelLoad > 3 && bigPointEaten)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (bigPointMarkersChecker[i])
                    {
                        Debug.Log("GO");
                        if (i >= 4)
                            i = 0;
                        navAgent.SetDestination(bigPointMarker[i + 1].position);
                        StartCoroutine(TimerBigPoint());
                    }
                }

            }
        }

        if (this.name == "Pinky")
        {
            if (Time.timeSinceLevelLoad > 10 && !bigPointEaten)
            {
                navAgent.SetDestination(playerPinky.position);
            }
            else if(Time.timeSinceLevelLoad > 10 && bigPointEaten)
            {
                for(int i = 0; i < 4; i++)
                {
                    if(bigPointMarkersChecker[i] == true)
                    {
                        if (i > 4)
                            i = 0;
                        navAgent.SetDestination(bigPointMarker[i].position);
                        StartCoroutine(TimerBigPoint());
                    }
                }     
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

            else if (Time.timeSinceLevelLoad > 15 && bigPointEaten)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (bigPointMarkersChecker[i] == true)
                    {
                        if (i > 4)
                            i = 0;
                        navAgent.SetDestination(bigPointMarker[i].position);
                        StartCoroutine(TimerBigPoint());
                    }
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

            else if (Time.timeSinceLevelLoad > 25 && bigPointEaten)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (bigPointMarkersChecker[i] == true)
                    {
                        if (i > 4)
                            i = 0;
                        navAgent.SetDestination(bigPointMarker[i].position);
                        StartCoroutine(TimerBigPoint());
                    }
                }
            }
        }
    }

    private IEnumerator TimerBigPoint()
    {
        yield return new WaitForSeconds(5);
        bigPointEaten = false;
    }
}
