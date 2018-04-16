using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChosenLevel : MonoBehaviour {
    
	void Awake () {
        FindObjectOfType<LoadLevel>().Load(ChosenLevel.levelIndex);
    }
}
