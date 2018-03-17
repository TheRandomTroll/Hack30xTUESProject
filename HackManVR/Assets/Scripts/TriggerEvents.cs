using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerEvents : MonoBehaviour
{

    public Movement movementScript;
    public PointsScript pointsScript;


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
            pointsScript.points++;
            Destroy(collider.gameObject);

        }

        if (collider.tag == "Ghost")
        {
            Debug.Log("Game Over");
            StartCoroutine(Restart());
        }
    }

    private void OnTriggerExit(Collider collider)
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

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
