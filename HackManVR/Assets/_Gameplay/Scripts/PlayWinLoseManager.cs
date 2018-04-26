using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayWinLoseManager : WinLoseManager {

    [SerializeField] private string winScene;
    [SerializeField] private int levelCount;
    


    public override void WinExecution()
    {
        ChosenLevel.levelIndex++;
        FindObjectOfType<PointManager>().SavePoints();
        if (ChosenLevel.levelIndex < levelCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            ChosenLevel.levelIndex = 0;
            SceneManager.LoadScene(winScene);
        }
    }


    public override void LoseExecution()
    {
        ChosenLevel.levelIndex = 0;
        FindObjectOfType<PointManager>().ResetPoints();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
