using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Clyde : Ghost {

    [SerializeField] private int distanceBetweenSwitching = 5;



    protected override void PursueBehaviour()
    {
        float distance = Vector3.Distance(pacman.transform.position, transform.position);
        distance /= map.GetGridSize();

        Debug.Log("Player distance to Clyde: " + distance);
       
        if (distance >= distanceBetweenSwitching)
        {
            if(currentTravelDestination != null && currentTravelDestination != map.GetWaypointOfObject(gameObject))
                return; // Wait to get to navAgent destination.
            navAgent.SetDestination(pacman.transform.position);
            currentTravelDestination = null;
        }
        else if (currentTravelDestination == null || currentTravelDestination == map.GetWaypointOfObject(gameObject))
        {
            Debug.LogWarning("Clyde entered other state!");
            HashSet<Waypoint> waypoints;

            // TODO: Gameplaywise decide should there be a random chance Clyde goes nearby the player
            int random = UnityEngine.Random.Range(0, 100);
            if (random <= 25) // Move closer to player
                waypoints = map.GetAllWaypointsInRange(pacman.gameObject, 5, 8);
            else // Go to a random place
                waypoints = map.GetAllWaypointsInRange(gameObject, 5, 8);
            
            // TODO: Remove sprite renderer stuff
            if (currentTravelDestination)
                currentTravelDestination.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            Waypoint waypoint = waypoints.ElementAt(Random.Range(0, waypoints.Count));
            waypoint.GetComponent<MeshRenderer>().material.color = Color.magenta;
            currentTravelDestination = waypoint;
            navAgent.SetDestination(waypoint.transform.position);
        }
    }
}
