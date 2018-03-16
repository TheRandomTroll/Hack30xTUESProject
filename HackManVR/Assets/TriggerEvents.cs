using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI

public class TriggerEvents : MonoBehaviour
{

    public Movement movementScript;
    public Text pointsScript;


    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Obstacle")
        {
            if (this.name == "detectorFront")
            {
                movementScript.canMove = false;
            }

            if (this.name == "detectorBack")
            {
                movementScript.rotateBack = false;
            }

            if (this.name == "detectorLeft")
            {

                movementScript.rotateLeft = false;
            }

            if (this.name == "detectorRight")
            {
                movementScript.rotateRight = false;
            }
        }

        if(collider.tag == "Point")
        {
            points.
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
}
