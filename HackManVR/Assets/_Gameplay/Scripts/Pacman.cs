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

    [SerializeField] private float speed = 4;
    [SerializeField] private float bigpointSpeed = 6;
    [SerializeField] private float bigpointDuration = 10;

    
    private int currentBigpointDuration = 0;

    private CustomInputManager inputManager;


    private void Start()
    {
        inputManager = FindObjectOfType<CustomInputManager>();
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if ((inputManager.GetLeftDown() || Input.GetKeyDown(KeyCode.A)) && rotateLeft)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
        }

        if ((inputManager.GetRightDown() || Input.GetKeyDown(KeyCode.D)) && rotateRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
        }
    }


    public void EatBigpoint()
    {
        foreach(Ghost ghost in FindObjectsOfType<Ghost>())
        {
            ghost.PlayerHasEatenBigPoint();
        }
        StartCoroutine(BigpointDuration());
    }

    private IEnumerator BigpointDuration()
    {
        //speed *= 1.5f;
        yield return new WaitForSeconds(bigpointDuration);
        //speed /= 1.5f;
        // TODO: Fix speed.
    }


    public float GetBigPointDuration()
    {
        return bigpointDuration;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ghost")
        {
            if (other.GetComponent<Ghost>().CanEatPlayer())
            {
                Debug.Log("Got killed by: " + other.name);
                FindObjectOfType<WinLoseManager>().LoseExecution();
            }
            else
            {
                FindObjectOfType<PointManager>().AddPoints(1000); // TODO: Add SerializeField.
                // V takiva momenti kato komentiram da addna serializefield osuznavam kolko sum murzeliv
                // i zapochvam da se chudq kak vuobshte si otvarqm ochite sutrinta.
                // dazhe mozheshe da go dobavq serializefield dokato pisha tozi komentar.
                // mozheshe i sega...
                // i sega.....
                // :'(
                other.GetComponent<Ghost>().GetEaten();
                Debug.Log("Ate: " + other.name);
            }
        }
    }
}
