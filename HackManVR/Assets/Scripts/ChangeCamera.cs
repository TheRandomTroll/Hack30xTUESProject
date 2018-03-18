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
        firstPersonCamera = GameObject.FindGameObjectWithTag("firstPrsCamera").GetComponent<Camera>();
        staticCamera = GameObject.FindGameObjectWithTag("staticCamera").GetComponent<Camera>();
        rawImage = GameObject.FindGameObjectWithTag("rawImage").GetComponent<RawImage>();
    }

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("camera", 1) == 1)
            firstPersonCamera.enabled = true;
        else if(PlayerPrefs.GetInt("camera", 1) == 2)
            staticCamera.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Select"))
        {
            staticCamera.enabled = !staticCamera.enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
            rawImage.enabled = !rawImage.enabled;
        }
    }
}
