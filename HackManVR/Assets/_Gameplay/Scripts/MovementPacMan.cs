using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementPacMan : MonoBehaviour {

    public bool rotateBack = true;
    public bool rotateForward = true;
    public bool rotateLeft = true;
    public bool rotateRight = true;

    public bool canMove = true;

    [SerializeField] private float speed = 5;

    private int way;

    public bool bigPointEaten = false;
    public bool isPaused;

    public Canvas pauseMenu;

    void Start () {

    }

    void Update()
    {

        if(Input.GetKey(KeyCode.Space))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Forward") && rotateForward && way == 3)
        {

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        }

        if (Input.GetButtonDown("Backward") && rotateBack && way != 3)
        {

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);

            way = 3;
        }

        if (Input.GetButtonDown("Left") && rotateLeft)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);

            way = 4;

        }

        if (Input.GetButtonDown("Right") && rotateRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
            way = 2;
        }


        if (Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
