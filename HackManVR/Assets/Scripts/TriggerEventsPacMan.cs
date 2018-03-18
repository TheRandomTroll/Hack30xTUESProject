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

  

    private void OnTriggerEnter(Collider collider)
    {
        MainAudio = GetComponent<AudioSource>();
        if (collider.tag == "Obstacle")
        {
            if (name == "detectorFront")
            {
                movementScriptPacMan.canMove = false;
            }

            if (name == "detectorBack")
            {
                movementScriptPacMan.rotateBack = false;
            }

            if (name == "detectorLeft")
            {

                movementScriptPacMan.rotateLeft = false;
            }

            if (name == "detectorRight")
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

        if (collider.tag == "Ghost")
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

        if(collider.tag == "Portal")
        {
            if(collider.name == "PORTAL1")
            {
                portal2.GetComponent<Collider>().enabled = false;
                player.position = portal2.position;
            }

            else if (collider.name == "PORTAL2")
            {
                portal1.GetComponent<Collider>().enabled = false;
                player.position = portal1.position;
            }
        }

        if(collider.tag == "BigPoint")
        {
            movementScriptBlinky.bigPointEaten = true;
            movementScriptPinky.bigPointEaten = true;
            movementScriptInky.bigPointEaten = true;
            movementScriptClyde.bigPointEaten = true;
            MainAudio.clip = BigPointSound;
            MainAudio.Play();
        }
        

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Obstacle")
        {
            if (this.name == "detectorFront")
            {
                movementScriptPacMan.canMove = true;
            }

            if (this.name == "detectorBack")
            {
                movementScriptPacMan.rotateBack = true;
            }

            if (this.name == "detectorLeft")
            {
                movementScriptPacMan.rotateLeft = true;
            }

            if (this.name == "detectorRight")
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
