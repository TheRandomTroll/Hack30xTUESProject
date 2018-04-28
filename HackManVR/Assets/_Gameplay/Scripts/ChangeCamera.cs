using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeCamera : MonoBehaviour {

    public Camera firstPersonCamera;
    public Camera staticCamera;
    public RawImage rawImage;

    private CustomInputManager inputManager;

    private void Awake()
    {
        firstPersonCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
        staticCamera = FindObjectOfType<CameraStatic>().GetComponentInChildren<Camera>();
        try
        {
            rawImage = GameObject.Find("RawImage").GetComponent<RawImage>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("No raw image :)");
        }
    }

    // Use this for initialization
    void Start () {
        staticCamera.enabled = false;
        firstPersonCamera.enabled = true;
        inputManager = FindObjectOfType<CustomInputManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(inputManager.GetRightTwoDown())
        {
            Debug.Log("Changing camera!");
        }
        if(inputManager.GetRightTwoDown() || Input.GetKeyDown(KeyCode.Mouse1))
        {
            staticCamera.enabled = !staticCamera.enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
            if(rawImage)
                rawImage.enabled = !rawImage.enabled;
            staticCamera.transform.eulerAngles = new Vector3(90, 0, 0);
            //rawImage.enabled = !rawImage.enabled;
        }
    }
}
