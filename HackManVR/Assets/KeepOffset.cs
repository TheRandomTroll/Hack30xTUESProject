using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOffset : MonoBehaviour {

    GameObject player;
    private Vector3 offset;

    

	void Start () {
        player = FindObjectOfType<CameraVRRay>().gameObject;
        if (!player) player = Camera.main.gameObject;
        if (!player) Debug.LogWarning("KeepOffset on " + name + " could not find player!");
        offset = transform.position - player.transform.position;
	}
	

	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
