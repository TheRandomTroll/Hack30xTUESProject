using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerEvents : MonoBehaviour
{

    public Movement movementScript;
    public PointsScript pointsScript;
    public Transform player;
    public Transform portal1;
    public Transform portal2;



    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Obstacle")
        {
            if (name == "detectorFront")
            {
                movementScript.canMove = false;
            }

            if (name == "detectorBack")
            {
                movementScript.rotateBack = false;
            }

            if (name == "detectorLeft")
            {

                movementScript.rotateLeft = false;
            }

            if (name == "detectorRight")
            {
                movementScript.rotateRight = false;
            }
        }

        if (this.tag == "Player" && collider.tag == "Point")
        {
            pointsScript.points += 20; 
            Destroy(collider.gameObject);

        }

        if (this.tag == "Player" && collider.tag == "Ghost")
        {
            Debug.Log("Game Over");
            StartCoroutine(Restart());
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
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Obstacle")
        {
            if (this.name == "detectorFront")
            {
                movementScript.canMove = true;
            }

            if (this.name == "detectorBack")
            {
                movementScript.rotateBack = true;
            }

            if (this.name == "detectorLeft")
            {
                movementScript.rotateLeft = true;
            }

            if (this.name == "detectorRight")
            {
                movementScript.rotateRight = true;
            }
        }

        //if (collider.tag == "Portal")
        //{
        //    if (collider.name == "PORTAL1")
        //    {
        //        portal2.GetComponent<Collider>().enabled = true;
        //    }

        //    else if (collider.name == "PORTAL2")
        //    {
        //        portal1.GetComponent<Collider>().enabled = true;
        //    }
        //}
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
