using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour {

    HasTriggered trigger;


    // TODO: Change for console.
    KeyCode main = KeyCode.Mouse0;

    [SerializeField]
    string alternativeInput = "Backward";

    [SerializeField]
    GetNumber getNumber;

    void Start () {
        trigger = GetComponent<HasTriggered>();
	}
	

	void Update () {
        if (trigger.GetTrigger() && (Input.GetKeyDown(main) || Input.GetButtonDown(alternativeInput))) {
            FindObjectOfType<LoadLevel>().Load(getNumber.GetIndex());
        }
    }
}
