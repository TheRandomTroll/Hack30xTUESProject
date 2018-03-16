using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Waypoint))]
[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour {

    Waypoint waypoint;

	void Start () {
        waypoint = GetComponent<Waypoint>();
	}
	

	void Update () {
        int gridSize = waypoint.gridSize;
        transform.position = new Vector3(
            Mathf.FloorToInt(transform.position.x / gridSize) * gridSize,
             waypoint.height,
            Mathf.FloorToInt(transform.position.z / gridSize) * gridSize);
	}
}
