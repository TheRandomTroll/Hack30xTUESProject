using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOffset : MonoBehaviour {
    [SerializeField]
    Transform player;
    private Vector3 offset;

    

	void Start () {
        offset = transform.position - player.position;
	}
	

	void LateUpdate () {
        transform.position = player.position + offset;
	}
}
