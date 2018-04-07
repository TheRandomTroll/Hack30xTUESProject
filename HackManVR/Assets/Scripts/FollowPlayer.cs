using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;

    public Camera firstPrsCamera;
    public Camera staticPrsCamera;

    [SerializeField]
    private float heightOffset = 1;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        firstPrsCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
        staticPrsCamera = GameObject.Find("Static Camera").GetComponent<Camera>();
    }

    void Update () {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position = player.position + new Vector3(0, heightOffset, 0);


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
