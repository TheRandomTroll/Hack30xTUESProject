using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public Camera staticPrsCamera;



    // Use this for initialization
    void Start () {
        staticPrsCamera.transform.eulerAngles = new Vector3(84, transform.eulerAngles.y, transform.eulerAngles.z);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

        if(staticPrsCamera.transform.eulerAngles.x == 0)
        {
            staticPrsCamera.transform.Rotate(new Vector3(85, staticPrsCamera.transform.eulerAngles.y, staticPrsCamera.transform.eulerAngles.z), Space.World);
        }
    }
}
