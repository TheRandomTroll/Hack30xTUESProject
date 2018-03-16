using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
