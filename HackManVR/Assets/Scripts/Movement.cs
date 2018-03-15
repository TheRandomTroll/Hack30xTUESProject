using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public bool rotateBack = true;
    public bool rotateForward = true;
    public bool rotateLeft = true;
    public bool rotateRight = true;

    public bool canMove = true;

    private float speed;

    private int way;

	void Start () {
        speed = 8;
	}
	
	void Update () {

        if(canMove)
        {
            transform.Translate(Vector3.forward / speed);
        }
            

        if(Input.GetKeyDown(KeyCode.W) && rotateForward && way == 3)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            way = 1;
        }

        if (Input.GetKeyDown(KeyCode.S) && rotateBack && way != 3)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
            way = 3;
        }

        if (Input.GetKeyDown(KeyCode.A) && rotateLeft)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
            way = 4;
        }

        if (Input.GetKeyDown(KeyCode.D) && rotateRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
            way = 2;
        }
    }
}
