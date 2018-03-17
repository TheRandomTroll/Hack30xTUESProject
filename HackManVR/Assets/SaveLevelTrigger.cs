using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLevelTrigger : MonoBehaviour {

    HasTriggered trigger;
    WorldGrid worldGrid;

    [SerializeField]
    GetNumber getNumber;

    // TODO: Change for console.
    KeyCode main = KeyCode.Mouse0;

    [SerializeField]
    string alternativeInput = "Backward";

    void Start () {
        trigger = GetComponent<HasTriggered>();
        worldGrid = FindObjectOfType<WorldGrid>();
	}
	

	void Update () {
        if (trigger.GetTrigger() && (Input.GetKeyDown(main) || Input.GetButtonDown(alternativeInput)))
        {
            SaveLevel.SerializeLevel(worldGrid, getNumber.GetIndex());
        }
    }
}
