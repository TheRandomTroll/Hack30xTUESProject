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
        Vector2Int gridPosition = GetGridPosition(target);
        return GetWaypointAt(gridPosition);
    }

    
    public int GetGridSize()
    {
        return gridSize;
    }


    public Vector2Int GetGridPosition(GameObject target)
    {
        return new Vector2Int(
            Mathf.RoundToInt(target.transform.position.x / gridSize), Mathf.RoundToInt(target.transform.position.z / gridSize));
    }

    public HashSet<Waypoint> GetAllWaypointsInRange(GameObject target, int minRange, int maxRange)
    {
        Vector2Int[] gridDirection = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};
        HashSet<Waypoint> waypoints = new HashSet<Waypoint>();
        Waypoint start = GetWaypointOfObject(target);
        HashSet<Waypoint> visitedWaypoints = new HashSet<Waypoint>();
        Queue<Waypoint> searchQueue = new Queue<Waypoint>();
        searchQueue.Enqueue(start);
        visitedWaypoints.Add(start);
        Debug.Log("Start point: " + GetGridPosition(start.gameObject));
        for(int i = 0; i < maxRange; i++)
        {
            List<Waypoint> loopQueue = new List<Waypoint>();
            while (searchQueue.Count != 0)
            {
                Waypoint toSearch = searchQueue.Dequeue();
                Vector2Int gridPosition = GetGridPosition(toSearch.gameObject);

                foreach (Vector2Int direction in gridDirection)
                {
                    Waypoint waypoint = GetWaypointAt(gridPosition + direction);
                    if (waypoint == null || visitedWaypoints.Contains(waypoint)) continue;
                    if (i >= minRange) {
                        waypoints.Add(waypoint);
                    }
                    visitedWaypoints.Add(waypoint);
                    loopQueue.Add(waypoint);
                }
            }

            for(int j = 0; j < loopQueue.Count; j++)
            {
                searchQueue.Enqueue(loopQueue[j]);
            }
        }
        return waypoints;
    }


}
