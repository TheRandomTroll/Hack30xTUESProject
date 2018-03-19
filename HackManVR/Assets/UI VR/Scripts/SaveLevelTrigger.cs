using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLevelTrigger : MonoBehaviour {

    UITrigger trigger;
    WorldGrid worldGrid;

    [SerializeField]
    GetNumber getNumber;

    void Start () {
        trigger = GetComponent<UITrigger>();
        worldGrid = FindObjectOfType<WorldGrid>();
	}
	

	void Update () {
        if (trigger.GetTrigger())
        {
            SaveLevel.SerializeLevel(worldGrid, getNumber.GetIndex());
        }
    }
}
