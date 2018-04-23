using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour {

    [SerializeField] public int points = 0; // Todo: remove SerializeField
    [SerializeField] private Text text;

    private void Start()
    {
        // Todo: AddPoints(previousLevel.points)
        text = GetComponent<Text>();
        UpdatePointText();
    }


    public void AddPoints(int points)
    {
        this.points += points;
        UpdatePointText();
    }


    private void UpdatePointText()
    {
        text.text = "Points: " + points;
    }
}
