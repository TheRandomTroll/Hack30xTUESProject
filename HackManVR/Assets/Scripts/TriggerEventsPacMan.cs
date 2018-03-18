using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerEventsPacMan : MonoBehaviour
{

    public MovementGhosts movementScriptBlinky;
    public MovementGhosts movementScriptPinky;
    public MovementGhosts movementScriptInky;
    public MovementGhosts movementScriptClyde;

    public MovementPacMan movementScriptPacMan;
    public PointsScript pointsScript;
    public Transform player;
    public Transform portal1;
    public Transform portal2;

    private AudioSource MainAudio;
    public AudioClip PointSound;
    public AudioClip BigPointSound;
    public AudioClip CherrySound;
    public AudioClip GameOverSound;

    private void Start()
    {
        movementScriptBlinky = GameObject.FindGameObjectWithTag("Blinky").GetComponent<MovementGhosts>();
        movementScriptPinky = GameObject.FindGameObjectWithTag("Pinky").GetComponent<MovementGhosts>();
        movementScriptInky = GameObject.FindGameObjectWithTag("Inky").GetComponent<MovementGhosts>();
        movementScriptClyde = GameObject.FindGameObjectWithTag("Clyde").GetComponent<MovementGhosts>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        movementScriptPacMan = player.GetComponent<MovementPacMan>();
        pointsScript = GameObject.FindGameObjectWithTag("Points").GetComponent<PointsScript>();
        portal1 = GameObject.FindGameObjectWithTag("PORTAL1").GetComponent<Transform>();
        portal2 = GameObject.FindGameObjectWithTag("PORTAL2").GetComponent<Transform>(); 
    }

    private void OnTriggerEnter(Collider collider)
    {
        MainAudio = GetComponent<AudioSource>();
        if (collider.tag == "Obstacle")
        {
            if (tag == "detectorFront")
            {
                movementScriptPacMan.canMove = false;
            }

            if (tag == "detectorBack")
            {
                movementScriptPacMan.rotateBack = false;
            }

            if (tag == "detectorLeft")
            {

                movementScriptPacMan.rotateLeft = false;
            }

            if (tag == "detectorRight")
            {
                movementScriptPacMan.rotateRight = false;
            }
        }

        if (collider.tag == "Point")
        {
            pointsScript.points += 20; 
            Destroy(collider.gameObject);
            MainAudio.clip = PointSound;
            MainAudio.Play();
        }

        if (collider.tag == "Blinky" || collider.tag == "Pinky" || collider.tag == "Inky" || collider.tag == "Clyde")
        {
            Debug.Log("Game Over");
            if(FindObjectOfType<ChangeCamera>().firstPersonCamera.enabled)
            {
                PlayerPrefs.SetInt("camera", 1);
            }

            else if(!FindObjectOfType<ChangeCamera>().firstPersonCamera.enabled)
            {
                Debug.Log("Second");
                PlayerPrefs.SetInt("camera", 2);
            }

            StartCoroutine(Restart());
            MainAudio.clip = GameOverSound;
            MainAudio.Play();
        }

        
        if(collider.tag == "PORTAL1")
        {
            portal2.GetComponent<Collider>().enabled = false;
            player.position = portal2.position;
        }

        else if (collider.tag == "PORTAL2")
        {
            portal1.GetComponent<Collider>().enabled = false;
            player.position = portal1.position;
        }
        

        if(tag == "Player" && collider.tag == "BigPoint")
        {
            movementScriptBlinky.bigPointEaten = true;
            movementScriptPinky.bigPointEaten = true;
            movementScriptInky.bigPointEaten = true;
            movementScriptClyde.bigPointEaten = true;
            pointsScript.points += 40;
            Destroy(collider.gameObject);
            MainAudio.clip = BigPointSound;
            MainAudio.Play();
        }
        

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Obstacle")
        {
            if (this.tag == "detectorFront")
            {
                movementScriptPacMan.canMove = true;
            }

            if (this.tag == "detectorBack")
            {
                movementScriptPacMan.rotateBack = true;
            }

            if (this.tag == "detectorLeft")
            {
                movementScriptPacMan.rotateLeft = true;
            }

            if (this.tag == "detectorRight")
            {
                movementScriptPacMan.rotateRight = true;
            }
        }

        
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
