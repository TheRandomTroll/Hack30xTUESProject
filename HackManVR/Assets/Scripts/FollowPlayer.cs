using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;

    public Camera firstPrsCamera;
    public Camera staticPrsCamera;
    Quaternion rotation;

    private void Awake()
    {
        rotation = transform.rotation;
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        
        if(staticPrsCamera.transform.eulerAngles.x == 0)
        {
            staticPrsCamera.transform.Rotate(new Vector3(85, staticPrsCamera.transform.eulerAngles.y, staticPrsCamera.transform.eulerAngles.z), Space.World);
        }

        if(firstPrsCamera.transform.eulerAngles.x < -40)
        {
            firstPrsCamera.transform.Rotate(new Vector3(0, firstPrsCamera.transform.eulerAngles.y, firstPrsCamera.transform.eulerAngles.z), Space.World);
        } 
    }
}
