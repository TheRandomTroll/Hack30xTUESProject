using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour {
    
    public enum GhostState { Frightened, Pursue, MovingAround, NotActivated}

    public GhostState currentState = GhostState.NotActivated;


    [SerializeField] private float activationTime;
    [SerializeField] private float bigpointDuration;

    private Vector3 startingPosition;
    [SerializeField] private Pacman pacman;
    public NavMeshAgent navAgent;


    void Start () {
        startingPosition = transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        pacman = FindObjectOfType<Pacman>();
        bigpointDuration = pacman.GetBigPointDuration();
        StartCoroutine(GhostActivation());
	}
	

	void Update () {
        if (currentState == GhostState.NotActivated) return;
        ExecuteGhostStateLogic();
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
        StartCoroutine(EnterFrightenedState());
    }

    private IEnumerator EnterFrightenedState()
    {
        GhostState previousState = currentState;
        currentState = GhostState.Frightened;
        yield return new WaitForSeconds(bigpointDuration);
        currentState = previousState;
    }


    protected virtual IEnumerator GhostActivation()
    {
        yield return new WaitForSeconds(activationTime);
        currentState = GhostState.MovingAround;
    }


    protected virtual void PursueBehaviour()
    {
        throw new NotImplementedException("Pursue behaviour should be implemented differently in every ghost");
    }

    protected virtual void FrightenedBehaviour()
    {
        Vector3 playerPosition = pacman.transform.position;
    }

    protected virtual void MovingAroundBehaviour()
    {

    }
}
