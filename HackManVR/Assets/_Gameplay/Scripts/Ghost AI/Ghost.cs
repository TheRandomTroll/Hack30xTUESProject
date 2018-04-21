using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System.Linq;

public class Ghost : MonoBehaviour {
    
    public enum GhostState { Frightened, Pursue, MovingAround, NotActivated}

    [SerializeField] public GhostState currentState = GhostState.NotActivated;

    [SerializeField] private GhostBehaviourTime[] ghostBehaviours;
    [SerializeField] private float currentStateTimer = 0;
    [SerializeField] private int currentStateIndex = 0;
    [SerializeField] private float bigpointDurationLeft = 0;


    [SerializeField] private float activationTime;
    private float bigpointDuration;

    protected Pacman pacman;
    protected NavMeshAgent navAgent;
    protected GroundMap map;

    private Vector3 startingPosition;

    [SerializeField] protected Waypoint currentTravelDestination;




    void Start () {
        startingPosition = transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        pacman = FindObjectOfType<Pacman>();
        bigpointDuration = pacman.GetBigPointDuration();
        map = FindObjectOfType<GroundMap>();
        StartCoroutine(GhostActivation());
	}
	

	void Update () {
        if (currentState == GhostState.NotActivated) return;
        StateSwitch();
        ExecuteGhostStateLogic();
	}

    private void StateSwitch()
    {
        if (currentState == GhostState.Frightened) return;
        currentStateTimer += Time.deltaTime;
        GhostBehaviourTime ghostBehaviour = ghostBehaviours[currentStateIndex];
        if(currentStateIndex < ghostBehaviours.Length - 1 && currentStateTimer >= ghostBehaviour.stateTime)
        {
            currentStateIndex++;
            currentStateTimer = 0;
            currentState = ghostBehaviours[currentStateIndex].state;
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

    private IEnumerator EnterFrightenedState()
    {

        bigpointDurationLeft += bigpointDuration;
        if (currentState != GhostState.Frightened)
        {
            GhostState previousState = currentState;
            currentState = GhostState.Frightened;
            Color previousColor = GetComponent<MeshRenderer>().material.color;
            GetComponent<MeshRenderer>().material.color = Color.blue;
            while (bigpointDurationLeft >= 0)
            {
                bigpointDurationLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            GetComponent<MeshRenderer>().material.color = previousColor;
            currentState = previousState;
        }
    }


    protected virtual IEnumerator GhostActivation()
    {
        yield return new WaitForSeconds(activationTime);
        currentState = GhostState.MovingAround;
        HashSet<Waypoint> waypoints = map.GetAllWaypointsInRange(gameObject, 0, 5);
        GhostBehaviourTime ghostBehaviour = ghostBehaviours[currentStateIndex];
        currentState = ghostBehaviour.state;
    }


    protected virtual void PursueBehaviour()
    {

        throw new NotImplementedException("Pursue behaviour should be implemented differently in every ghost");
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
