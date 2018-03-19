using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour {

    UITrigger trigger;

    [SerializeField]
    GetNumber getNumber;

    void Start () {
        trigger = GetComponent<UITrigger>();
	}
	

	void Update () {
        if (trigger.GetTrigger()) {
            FindObjectOfType<LoadLevel>().Load(getNumber.GetIndex());
        }
    }
}
