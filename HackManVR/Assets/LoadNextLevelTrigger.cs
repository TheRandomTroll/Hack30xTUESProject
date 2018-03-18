using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevelTrigger : MonoBehaviour {

    HasTriggered trigger;


    // TODO: Change for console.
    KeyCode main = KeyCode.Mouse0;

    [SerializeField]
    string alternativeInput = "Backward";

    [SerializeField]
    GetNumber getNumber;

    void Start()
    {
        trigger = GetComponent<HasTriggered>();
    }

    // Update is called once per frame
    void Update () {
        if (trigger.GetTrigger() && (Input.GetKeyDown(main) || Input.GetButtonDown(alternativeInput)))
        {
            FindObjectOfType<LoadLevel>().Load(0); // TODO: Actually change it dynamically. Currently only for demo.
            Destroy(gameObject);
        }
    }
}
