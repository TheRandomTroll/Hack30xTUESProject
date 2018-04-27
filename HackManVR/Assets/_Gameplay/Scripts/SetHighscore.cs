using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHighscore : MonoBehaviour {
    

	void Start () {
        int currentHighscore = PlayerPrefs.GetInt("highscore", 0);	
	}
}
