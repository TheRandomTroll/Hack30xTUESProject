using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost {

    [SerializeField] private int blocksAhead = 4;
    

    protected override void PursueBehaviour()
    {
        Waypoint targetWaypoint = GetBlockInFrontPosition(pacman.gameObject, blocksAhead);
        navAgent.SetDestination(targetWaypoint.transform.position);
    }

    private Waypoint GetBlockInFrontPosition(GameObject target, int blocks)
    {
        int gridSize = map.GetGridSize();
        Waypoint waypoint = map.GetWaypointOfObject(target); // If player is facing a wall the block beneath is returned.
        for (int i = blocks; i >= 1; i--)
        {
            Transform targetTransform = target.transform;
            Vector3 oldPosition = targetTransform.position;
            targetTransform.Translate(Vector3.forward * blocks * gridSize);
            Waypoint newWaypoint = map.GetWaypointAt(map.GetGridPosition(targetTransform.gameObject));
            targetTransform.position = oldPosition;
            if (newWaypoint)
            {
                waypoint = newWaypoint;
                break;
            }
        }

        return waypoint;
    }
}
