using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    
	void Start () {
        gameObject.GetComponentInChildren<TextMesh>().text = FindObjectOfType<GroundMap>().GetGridPosition(gameObject) + "";
	}
	
}
