using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMap : MonoBehaviour {

    [SerializeField] private int gridSize;

    private Dictionary<Vector2Int, Waypoint> map = new Dictionary<Vector2Int, Waypoint>();

	void Start () {
		foreach(Waypoint waypoint in FindObjectsOfType<Waypoint>())
        {
            Vector2Int gridPosition = new Vector2Int(
                (int)waypoint.transform.position.x / gridSize, (int)waypoint.transform.position.z / gridSize);
            map.Add(gridPosition, waypoint);
        }
	}

    public Waypoint GetWaypointAt(Vector2Int position)
    {
        if(map.ContainsKey(position))
        {
            return map[position];
        }
        return null;
    }
}
