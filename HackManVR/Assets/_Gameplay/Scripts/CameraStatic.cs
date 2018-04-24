using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStatic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(45, 90, 0);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(10, 20, 20);
	}
}
