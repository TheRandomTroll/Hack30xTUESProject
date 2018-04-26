using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {
    
	void Update () {
		if(Input.GetKeyDown(KeyCode.N) || (Input.GetButton("Left1") && Input.GetButton("Right1")))
        {
            CollectAllPointsCheat();
        }
	}

    private void CollectAllPointsCheat()
    {
        Point[] points = FindObjectsOfType<Point>();
        foreach(Point point in points)
        {
            FindObjectOfType<PointManager>().AddPoints(100);
        }
    }
}
