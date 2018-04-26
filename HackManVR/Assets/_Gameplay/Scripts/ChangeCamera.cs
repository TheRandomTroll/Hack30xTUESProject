using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeCamera : MonoBehaviour {

    public Camera firstPersonCamera;
    public Camera staticCamera;
    public RawImage rawImage;

    private void Awake()
    {
        firstPersonCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
        staticCamera = FindObjectOfType<CameraStatic>().GetComponentInChildren<Camera>();
        rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
    }

    // Use this for initialization
    void Start () {
        staticCamera.enabled = false;
        firstPersonCamera.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Select") || Input.GetKeyDown(KeyCode.Mouse1))
        {
            staticCamera.enabled = !staticCamera.enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
            rawImage.enabled = !rawImage.enabled;
            staticCamera.transform.eulerAngles = new Vector3(90, 0, 0);
            //rawImage.enabled = !rawImage.enabled;
        }
    }
}
