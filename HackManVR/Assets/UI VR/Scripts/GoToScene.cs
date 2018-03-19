using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UITrigger))]
public class GoToScene : MonoBehaviour {

    [SerializeField]
    private int sceneIndex = -1;
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private float waitTime = 0.1f;

    private UITrigger trigger;

    


    void Start()
    {
        trigger = GetComponent<UITrigger>();
    }


	void Update () {
        if(trigger.GetTrigger())
        {
            if(sceneIndex >= 0)
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
