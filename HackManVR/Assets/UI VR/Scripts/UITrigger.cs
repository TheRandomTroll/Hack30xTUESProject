using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UITrigger : MonoBehaviour {

    [SerializeField]
    private bool outsideTrigger = false; // TODO: remove SerializeField
    [SerializeField]
    private float startTriggerTimer = 0.1f;

    private float triggerTimer;

    [SerializeField]
    private KeyCode input = KeyCode.Mouse0;
    [SerializeField]
    private string alternative = "Backward";

    private bool isTriggered = false;
    private void Start()
    {
        triggerTimer = 0;
    }


    void Update()
    {
        if(triggerTimer > 0)
        {
            triggerTimer -= Time.deltaTime;
        }
        else
        {
            outsideTrigger = false;
        }
        isTriggered = false;
        if(outsideTrigger &&
               (Input.GetKeyDown(input) || Input.GetButtonDown(alternative)))
        {
            isTriggered = true;
        }

    }


    public bool GetTrigger()
    {
        return isTriggered;
    }

    public void Trigger()
    {
        triggerTimer = startTriggerTimer;
        outsideTrigger = true;
    }
}
