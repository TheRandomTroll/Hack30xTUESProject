using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {

    public Camera firstPersonCamera;
    public Camera staticCamera;
	// Use this for initialization
	void Start () {
        staticCamera.enabled = true;
        firstPersonCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Select"))
        {
            staticCamera.enabled = !staticCamera.enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
        }
    }
}
