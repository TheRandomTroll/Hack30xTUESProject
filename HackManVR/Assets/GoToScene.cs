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

    [SerializeField]
    private float waitTime = 0.1f;



    // TODO: Change for console.
    KeyCode main = KeyCode.Mouse0;

    [SerializeField]
    string alternativeInput = "Backward";


    void Start()
    {
        trigger = GetComponent<HasTriggered>();
    }


	void Update () {
        if(trigger.GetTrigger() && (Input.GetKey(main) || Input.GetButtonDown(alternativeInput)))
        {
            if(sceneIndex > 0)
            {
                StartCoroutine(LoadScene(sceneIndex));
            }
            else
            {
                StartCoroutine(LoadScene(sceneName));
            }
        }
	}


    private IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(index);
    }

    private IEnumerator LoadScene(string name)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(name);
    }
}
