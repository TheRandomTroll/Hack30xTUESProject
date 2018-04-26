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

    [SerializeField] private Color color;
    [SerializeField] private Color hoverColor;

    private bool isTriggered = false;
    private void Start()
    {
        color = GetComponent<TextMesh>().color;
        hoverColor = new Color(color.r + 0.15f, color.g - 0.15f, color.b - 0.15f);
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

        if(outsideTrigger)
        {
            GetComponent<TextMesh>().color = hoverColor;
        }
        else
        {
            GetComponent<TextMesh>().color = color;
        }


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
