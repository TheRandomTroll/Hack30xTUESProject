using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelNow : MonoBehaviour {
    
	void Start () {
        Invoke("LoadLevel", 0.5f);
	}

    public void LoadLevel()
    {
        FindObjectOfType<LoadLevel>().Load(0);
    }
}
