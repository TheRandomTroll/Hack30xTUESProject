using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {


    private CustomInputManager inputManager;

    private void Start()
    {
        inputManager = FindObjectOfType<CustomInputManager>();
    }


    void Update () {
		if(Input.GetKeyDown(KeyCode.N) || (inputManager.GetLeftOne() && inputManager.GetRightOne()))
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
