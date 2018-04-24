using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pacman : MonoBehaviour {

    public bool rotateBack = true;
    public bool rotateForward = true;
    public bool rotateLeft = true;
    public bool rotateRight = true;

    public bool canMove = true;

    [SerializeField] private float speed = 5;
    [SerializeField] private float bigpointDuration = 10;

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


    public void EatBigpoint()
    {
        bigPointEaten = true;
        foreach(Ghost ghost in FindObjectsOfType<Ghost>())
        {
            ghost.PlayerHasEatenBigPoint();
        }
        StartCoroutine(BigpointDuration());
    }

    private IEnumerator BigpointDuration()
    {
        yield return new WaitForSeconds(bigpointDuration);
        bigPointEaten = false;
    }


    public float GetBigPointDuration()
    {
        return bigpointDuration;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ghost")
        {
            if (bigPointEaten)
            {
                other.GetComponent<Ghost>().GetEaten();
            }
            else
            {
                FindObjectOfType<WinLoseManager>().LoseExecution();
            }
        }
    }
}
