using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System.Linq;

public class Ghost : MonoBehaviour {
    
    public enum GhostState { Frightened, Pursue, MovingAround, NotActivated, Respawning }

    [SerializeField] public GhostState currentState = GhostState.NotActivated;

    [SerializeField] private GhostBehaviourTime[] ghostBehaviours;
    [SerializeField] private float currentStateTimer = 0;
    [SerializeField] private int currentStateIndex = 0;
    [SerializeField] private float bigpointDurationLeft = 0;

    [SerializeField] private float blinkStartTime = 2f;
    [SerializeField] private float blinkFrequency = 1;

    [SerializeField] private float activationTime;

    [SerializeField] private Color ghostColor;
    private float bigpointDuration;

    protected Pacman pacman;
    protected NavMeshAgent navAgent;
    protected GroundMap map;

    private Waypoint startingWaypoint;

    [SerializeField] protected Waypoint currentTravelDestination;


    void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        pacman = FindObjectOfType<Pacman>();
        bigpointDuration = pacman.GetBigPointDuration();
        map = FindObjectOfType<GroundMap>();
        StartCoroutine(GhostActivation());
        ghostColor = GetComponent<MeshRenderer>().material.color;

        navAgent.autoBraking = true;
	}
	

	void Update () {
        if (currentState == GhostState.NotActivated || currentState == GhostState.Respawning) return;
        StateSwitch();
        ExecuteGhostStateLogic();
	}

    private void StateSwitch()
    {
        if (currentState == GhostState.Frightened || currentState == GhostState.Respawning) return;
        currentStateTimer += Time.deltaTime;
        GhostBehaviourTime ghostBehaviour = ghostBehaviours[currentStateIndex];
        if(currentStateIndex < ghostBehaviours.Length - 1 && currentStateTimer >= ghostBehaviour.stateTime)
        {
            currentStateIndex++;
            currentStateTimer = 0;
            currentState = ghostBehaviours[currentStateIndex].state;
            if(currentTravelDestination)
                currentTravelDestination.GetComponent<MeshRenderer>().material.color = Color.green;
            currentTravelDestination = null;
            navAgent.SetDestination(gameObject.transform.position); // Stop
        }
    }

    private void ExecuteGhostStateLogic()
    {
        if (currentState == GhostState.MovingAround)
        {
            MovingAroundBehaviour();
        }
        else if (currentState == GhostState.Frightened)
        {
            FrightenedBehaviour();
        }
        else if (currentState == GhostState.Pursue)
        {
            PursueBehaviour(); // Individual for every ghost.
        }
    }

    public void PlayerHasEatenBigPoint()
    {
        if (currentState == GhostState.NotActivated) return;
        StartCoroutine(EnterFrightenedState());
    }

    public virtual void GetEaten()
    {
        // URGENT TODO:
        // Otiva obratno mnogo burzo v starting position.
        // Premigva dokato go pravi.
        StartCoroutine(EatenCoroutine());
    }

    private IEnumerator EatenCoroutine()
    {
        currentState = GhostState.Respawning;
        Waypoint currentWaypoint = map.GetWaypointOfObject(gameObject);
        navAgent.SetDestination(startingWaypoint.transform.position);
        float startSpeed = navAgent.speed;
        navAgent.speed *= 10;
        GetComponent<BoxCollider>().enabled = false;
        startingWaypoint.GetComponent<MeshRenderer>().material.color = ghostColor;
        while (currentWaypoint != startingWaypoint)
        {
            currentWaypoint = map.GetWaypointOfObject(gameObject);
            Debug.Log("is in eaten Coroutine");
            yield return new WaitForEndOfFrame();
        }

        navAgent.speed = startSpeed;
        Debug.Log("exited EatenCoroutine!");
        currentState = ghostBehaviours[currentStateIndex].state;

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<MeshRenderer>().material.color = ghostColor;
        GetComponent<BoxCollider>().enabled = true;
    }


    protected virtual IEnumerator GhostActivation()
    {
        yield return new WaitForSeconds(activationTime);
        currentState = GhostState.MovingAround;
        startingWaypoint = map.GetWaypointOfObject(gameObject);
        HashSet<Waypoint> waypoints = map.GetAllWaypointsInRange(gameObject, 0, 5);
        GhostBehaviourTime ghostBehaviour = ghostBehaviours[currentStateIndex];
        currentState = ghostBehaviour.state;
    }


    protected virtual void PursueBehaviour()
    {

        throw new NotImplementedException("Pursue behaviour should be implemented differently in every ghost");
    }


    private IEnumerator EnterFrightenedState()
    {
        if (currentState == GhostState.Respawning) yield break;
        bigpointDurationLeft += bigpointDuration;

        // in case bigpoint eaten a second time!
        GetComponent<MeshRenderer>().material.color = ghostColor; 
        float blinkMax = 1 / blinkFrequency;
        float blinkTimer = 0;

        if (currentState != GhostState.Frightened)
        {
            currentState = GhostState.Frightened;
            GetComponent<MeshRenderer>().material.color = Color.blue;


            
            while (bigpointDurationLeft >= 0)
            {
                bigpointDurationLeft -= Time.deltaTime;
                if (bigpointDurationLeft <= blinkStartTime)
                {
                    blinkTimer += Time.deltaTime;
                    if (blinkTimer >= blinkMax)
                    {
                        MeshRenderer renderer = GetComponent<MeshRenderer>();
                        if (renderer.material.color == ghostColor)
                        {
                            renderer.material.color = Color.blue;
                        }
                        else
                        {
                            renderer.material.color = ghostColor;
                        }
                        blinkTimer = 0;
                    }

                    if(currentState == GhostState.Respawning)
                    {
                        yield break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            GetComponent<MeshRenderer>().material.color = ghostColor;
            currentState = ghostBehaviours[currentStateIndex].state;

            currentTravelDestination = null;
            navAgent.SetDestination(transform.position); // Stop
        }
    }


    protected virtual void FrightenedBehaviour()
    {
        if (currentTravelDestination == null || map.GetWaypointOfObject(gameObject) == currentTravelDestination)
        {
            // TODO: da vidq dali shte uvelicha range-a ako pacmana e daleche.
            HashSet<Waypoint> pacmanWaypointsRange = map.GetAllWaypointsInRange(pacman.gameObject, 0, 6);
            HashSet<Waypoint> ghostWaypoints = map.GetAllWaypointsInRange(gameObject, 4, 6);

            
            ghostWaypoints.ExceptWith(pacmanWaypointsRange);
            // TODO: Remove
            for (int i = 0; i < ghostWaypoints.Count; i++)
            {
                ghostWaypoints.ElementAt(i).GetComponent<MeshRenderer>().material.color = Color.red;
            }

            int randomIndex = UnityEngine.Random.Range(0, ghostWaypoints.Count);
            if (currentTravelDestination) currentTravelDestination.GetComponent<MeshRenderer>().material.color = Color.green;
            currentTravelDestination = ghostWaypoints.ElementAt(randomIndex);
            currentTravelDestination.GetComponent<MeshRenderer>().material.color = Color.blue;
            navAgent.SetDestination(currentTravelDestination.transform.position);
        }


    }

    protected virtual void MovingAroundBehaviour()
    {
        if (currentTravelDestination == null || map.GetWaypointOfObject(gameObject) == currentTravelDestination) {
            HashSet<Waypoint> waypoints = map.GetAllWaypointsInRange(gameObject, 10, 15);
            if(waypoints.Count == 0) // Ako ne e tolkoz golqm mapa.
                waypoints = map.GetAllWaypointsInRange(gameObject, 5, 10);
            if (waypoints.Count == 0) // Veche sled tova nqma nikakva vuzmozhnost da ne se nameri path
                waypoints = map.GetAllWaypointsInRange(gameObject, 0, 5);

            int randomIndex = UnityEngine.Random.Range(0, waypoints.Count);

            // TODO: Remove things with meshrenderer
            if (currentTravelDestination) currentTravelDestination.GetComponent<MeshRenderer>().material.color = Color.green;
            currentTravelDestination = waypoints.ElementAt(randomIndex);
            currentTravelDestination.GetComponent<MeshRenderer>().material.color = Color.blue;
            navAgent.SetDestination(currentTravelDestination.transform.position);
        }
    }
}
