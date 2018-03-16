using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Vector3 offset;

    public Transform player;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x, 8, player.position.z - 5);
    }
}
