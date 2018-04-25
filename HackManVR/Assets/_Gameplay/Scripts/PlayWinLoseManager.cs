using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayWinLoseManager : WinLoseManager {

    [SerializeField] private string winScene;
    [SerializeField] private int levelCount;

    private void Start()
    {

    }

    public override void WinExecution()
    {
        if (ChosenLevel.levelIndex < levelCount)
        {

            ChosenLevel.levelIndex++;
            FindObjectOfType<PointManager>().SavePoints();
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
        SceneManager.LoadScene(ChosenLevel.levelIndex);
    }
}
