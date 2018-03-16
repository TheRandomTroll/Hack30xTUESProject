using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HasTriggered : MonoBehaviour {

    [SerializeField]
    private bool isTriggered = false; // TODO: remove SerializeField
    [SerializeField]
    private float startTriggerTimer = 0.1f;

    private float triggerTimer;

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
            isTriggered = false;
        }

        if (GetTrigger() && Input.GetButtonDown("Backward"))
        {
            if(gameObject.name == "Start")
            {
                SceneManager.LoadScene(1);
            }

            if (gameObject.name == "OPTION 1")
            {
                
            }

            if (gameObject.name == "OPTION 2")
            {
                
            }

        }
    }


    public bool GetTrigger()
    {
        return isTriggered;
    }

    public void Trigger()
    {
        triggerTimer = startTriggerTimer;
        isTriggered = true;
    }
}
