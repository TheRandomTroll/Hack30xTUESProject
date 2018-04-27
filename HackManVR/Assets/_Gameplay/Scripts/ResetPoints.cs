using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPoints : MonoBehaviour {
    

	void Start () {
        PlayerPrefs.SetInt("points", 0);
	}
}
