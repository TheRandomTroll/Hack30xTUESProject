using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomLevelManager : WinLoseManager {

    [SerializeField] private string levelPickerSceneName = "LevelPicker";


    public override void LoseExecution()
    {
        // TODO: Send the player up in the sky and activate a lose UI screen.
        Debug.Log("lose!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload!
    }

    public override void WinExecution()
    {
        // TODO: Send the player up in the sky and activate a win UI screen.
        Debug.Log("victory!");
        SceneManager.LoadScene(levelPickerSceneName);
    }
}
