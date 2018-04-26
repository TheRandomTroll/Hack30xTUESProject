using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour {

    [SerializeField] string scene;
    [SerializeField] string buttonName = "Back";
    [SerializeField] KeyCode keycode = KeyCode.Escape;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(keycode) || Input.GetButtonDown(buttonName))
        {
            Debug.Log("Exit scene!");
            SceneManager.LoadScene(scene);
        }
	}
}
