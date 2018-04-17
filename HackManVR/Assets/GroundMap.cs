using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMap : MonoBehaviour {

    [SerializeField] private int gridSize;
    [SerializeField] private Vector4 gridBounds = Vector4.zero; // (minX, minY, maxX, maxY)

    private Dictionary<Vector2Int, Waypoint> map = new Dictionary<Vector2Int, Waypoint>();

	void Start () {
        Waypoint randomWaypoint = FindObjectOfType<Waypoint>();
        int minGridX = (int) randomWaypoint.transform.position.x / gridSize;
        int minGridY = (int)randomWaypoint.transform.position.z / gridSize;
        int maxGridX = (int)randomWaypoint.transform.position.x / gridSize;
        int maxGridY = (int)randomWaypoint.transform.position.z / gridSize;

		foreach(Waypoint waypoint in FindObjectsOfType<Waypoint>())
        {
            Vector2Int gridPosition = new Vector2Int(
                (int)waypoint.transform.position.x / gridSize, (int)waypoint.transform.position.z / gridSize);
            map.Add(gridPosition, waypoint);
            Debug.Log("Add waypoint at " + waypoint.transform.position + " to " + gridPosition);

            int x = gridPosition.x;
            int y = gridPosition.y;

            if (minGridX > x) minGridX = x;
            if (minGridY > y) minGridY = y;
            if (maxGridX < x) maxGridX = x;
            if (maxGridY < y) maxGridY = y;
        }

        gridBounds = new Vector4(minGridX, minGridY, maxGridX, maxGridY);
	}

    public Waypoint GetWaypointAt(Vector2Int position)
    {
        if(map.ContainsKey(position))
        {
            return map[position];
        }
        return null;
    }


    public Waypoint GetWaypointOfObject(GameObject target)
    {
        Vector2Int gridPosition = new Vector2Int(
            (int)target.transform.position.x / gridSize, (int)target.transform.position.z / gridSize);
        return GetWaypointAt(gridPosition);
    }
}
