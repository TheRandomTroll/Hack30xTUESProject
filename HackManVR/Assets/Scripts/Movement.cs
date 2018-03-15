using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private float speed;
    private int way;
	// Use this for initialization
	void Start () {
        speed = 8;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward / speed);

        if(Input.GetKeyDown(KeyCode.W) && way == 3)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
            way = 1;
        }

        if (Input.GetKeyDown(KeyCode.S) && way != 3)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
            way = 3;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
            way = 4;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
            way = 2;
        }
    }
}
