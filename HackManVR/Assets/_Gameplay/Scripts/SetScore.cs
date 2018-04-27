using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScore : MonoBehaviour {

    [SerializeField] private TextMesh highscoreText;
    [SerializeField] private TextMesh scoreText;

    void Start () {
        int points = PlayerPrefs.GetInt("points");
        scoreText.text = "Score\n" + points;

        int highscore = PlayerPrefs.GetInt("highscore", 0);
        if(points > highscore)
        {
            highscore = points;
            PlayerPrefs.SetInt("highscore", points);
        }

        highscoreText.text = "Highscore\n" + points;

        PlayerPrefs.SetInt("points", 0);
    }
	
}
