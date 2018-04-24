using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerEventsPacMan : MonoBehaviour
{
    

    public Pacman movementScriptPacMan;
    public Transform player;

    private void Start()
    {
        movementScriptPacMan = FindObjectOfType<Pacman>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Wall")
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
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Wall")
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
}
