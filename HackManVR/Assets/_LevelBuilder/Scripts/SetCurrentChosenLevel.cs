using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentChosenLevel : MonoBehaviour {

    private UITrigger trigger;

	void Start () {
        trigger = GetComponent<UITrigger>();
	}
	

	void Update () {
		if(trigger.GetTrigger())
        {
            ChosenLevel.levelIndex = FindObjectOfType<GetNumber>().GetIndex();
        }
	}
}
