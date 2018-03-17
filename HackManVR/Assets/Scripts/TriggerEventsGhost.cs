﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerEventsGhost : MonoBehaviour
{

    public MovementPacMan movementScriptPacMan;
    public Transform player;
    public Transform portal1;
    public Transform portal2;



    private void OnTriggerEnter(Collider collider)
    {

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
   
    }
}
