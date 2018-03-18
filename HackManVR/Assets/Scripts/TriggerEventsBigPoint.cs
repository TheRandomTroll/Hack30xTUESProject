using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerEventsBigPoint : MonoBehaviour
{

    public MovementGhosts movementScriptBlinky;
    public MovementGhosts movementScriptPinky;
    public MovementGhosts movementScriptInky;
    public MovementGhosts movementScriptClyde;

    private void Awake()
    {
        movementScriptBlinky = GameObject.Find("Blinky").GetComponent<MovementGhosts>();
        movementScriptPinky = GameObject.Find("Pinky").GetComponent<MovementGhosts>();
        movementScriptInky = GameObject.Find("Inky").GetComponent<MovementGhosts>();
        movementScriptClyde = GameObject.Find("Clyde").GetComponent<MovementGhosts>();

    }


    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Player")
        {
            if (name == "Marker 1")
            {
                movementScriptBlinky.bigPointMarkersChecker[0] = true;
                movementScriptPinky.bigPointMarkersChecker[0] = true;
                movementScriptInky.bigPointMarkersChecker[0] = true;
                movementScriptClyde.bigPointMarkersChecker[0] = true;
            }

            if (name == "Marker 2")
            {
                movementScriptBlinky.bigPointMarkersChecker[1] = true;
                movementScriptPinky.bigPointMarkersChecker[1] = true;
                movementScriptInky.bigPointMarkersChecker[1] = true;
                movementScriptClyde.bigPointMarkersChecker[1] = true;
            }

            if (name == "Marker 3")
            {
                movementScriptBlinky.bigPointMarkersChecker[2] = true;
                movementScriptPinky.bigPointMarkersChecker[2] = true;
                movementScriptInky.bigPointMarkersChecker[2] = true;
                movementScriptClyde.bigPointMarkersChecker[2] = true;
            }

            if (name == "Marker 4")
            {
                movementScriptBlinky.bigPointMarkersChecker[3] = true;
                movementScriptPinky.bigPointMarkersChecker[3] = true;
                movementScriptInky.bigPointMarkersChecker[3] = true;
                movementScriptClyde.bigPointMarkersChecker[3] = true;
            }
        }

    }
}
