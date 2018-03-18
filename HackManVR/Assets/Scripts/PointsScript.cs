using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointsScript : MonoBehaviour {

    public string text;
    public int points;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(name == "Points")
        {
            GetComponent<Text>().text = points.ToString();
        }
        if(name == "Status")
            GetComponent<Text>().text = text;
	}
}
