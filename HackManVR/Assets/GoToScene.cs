using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HasTriggered))]
public class GoToScene : MonoBehaviour {

    [SerializeField]
    private int sceneIndex = -1;
    
    [SerializeField]
    private string sceneName;

    private HasTriggered trigger;


    // TODO: Change for console.
    KeyCode leftClick = KeyCode.Mouse0;


    void Start()
    {
        trigger = GetComponent<HasTriggered>();
    }


	void Update () {
        if(trigger.GetTrigger() && Input.GetKey(leftClick))
        {
            if(sceneIndex > 0)
            {
                LoadScene(sceneIndex);
            }
            else
            {
                LoadScene(sceneName);
            }
        }
	}


    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


}
