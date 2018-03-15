using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEvents : MonoBehaviour
{

    public Movement movementScript;

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
