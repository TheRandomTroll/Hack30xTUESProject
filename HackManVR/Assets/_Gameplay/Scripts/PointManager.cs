using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour {

    [SerializeField] public int points = 0; // Todo: remove SerializeField
    [SerializeField] private Text text;
    [SerializeField] private int pointCount; // TODO: Remove SerializeField

    private void Start()
    {
        points = GetSavedPoints();
        pointCount = FindObjectsOfType<Point>().Length;
        text = GetComponent<Text>();
        UpdatePointText();
    }


    public void AddPoints(int points)
    {
        this.points += points;
        pointCount--;
        UpdatePointText();

        if(pointCount <= 0)
        {
            FindObjectOfType<WinLoseManager>().WinExecution();
        }
    }


    private void UpdatePointText()
    {
        text.text = "Points: " + points;
    }


    public int GetSavedPoints()
    {
        return PlayerPrefs.GetInt("points", 0); 
    }

    public void SavePoints()
    {
        PlayerPrefs.SetInt("points", points);
    }


    public void ResetPoints()
    {
        points = 0;
        PlayerPrefs.SetInt("points", 0);
    }
}
