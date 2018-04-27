using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky : Ghost {

    [SerializeField] private Vector3 setPosition; // DEBUGGING ONLY. TODO: Remove


    protected override void PursueBehaviour()
    {
        int gridSize = map.GetGridSize();

        // TODO: Vsichkite tezi neshta kudeto gi prava s vrushtane na stari propertita na transform.
        // Da namera nqkakuv po umen nachin.
        Transform blinky = FindObjectOfType<Blinky>().transform;

        Waypoint blockInFrontOfPacman = GetBlockInFront(pacman.gameObject);

        if(blockInFrontOfPacman == null)
        {
            Debug.LogError("BlockInFrontOfPacmanNotFound!");
        }
        /*
        Vector3 oldRotation = blinky.rotation.eulerAngles;
        Vector3 oldPosition = blinky.transform.position;

        Vector3 lookAtPosition = blockInFrontOfPacman.gameObject.transform.position;
        lookAtPosition.y = transform.position.y;
        blinky.LookAt(lookAtPosition);
        */
        Vector3 target = (Vector3.Normalize(blockInFrontOfPacman.transform.position - blinky.transform.position) * 2 
            * Vector3.Distance(blockInFrontOfPacman.transform.position, blinky.transform.position));

        target.y = blinky.transform.position.y;

        Debug.DrawRay(blinky.transform.position, target, Color.yellow);

        setPosition = target;
        navAgent.SetDestination(target);
    }

    private Waypoint GetBlockInFront(GameObject target)
    {
        Transform targetTransform = target.transform;
        Vector3 oldPosition = targetTransform.position;
        Waypoint waypoint = map.GetWaypointOfObject(target);
        targetTransform.Translate(Vector3.forward * map.GetGridSize());
        Waypoint frontWaypoint = map.GetWaypointAt(map.GetGridPosition(targetTransform.gameObject));
        if(frontWaypoint)
        {
            waypoint = frontWaypoint;
        }
        targetTransform.position = oldPosition;

        return waypoint;
    }
}
