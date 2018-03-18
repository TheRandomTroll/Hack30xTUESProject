using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour {


    public Button Exit;

	// Use this for initialization
	void Start () {
        Exit = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        Exit.onClick.AddListener(ExitScene);
	}

    void ExitScene()
    {
        SceneManager.LoadScene(0);
    }
}
