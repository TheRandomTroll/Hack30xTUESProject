using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost {

    protected override void PursueBehaviour()
    {
        navAgent.SetDestination(pacman.transform.position);
        currentTravelDestination = map.GetWaypointOfObject(pacman.gameObject); // Do only for debugging purposes
    }
}
