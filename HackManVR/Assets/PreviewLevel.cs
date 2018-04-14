using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewLevel : MonoBehaviour {

    [SerializeField] private GameObject levelParent;
    private GetNumber getNumberUI;
    private GenerateLevel levelGenerator;
    private LoadLevel levelLoader;

    private int lastFrameLevelIndex;

	void Start () {
        getNumberUI = FindObjectOfType<GetNumber>();
        levelGenerator = FindObjectOfType<GenerateLevel>();
        levelLoader = FindObjectOfType<LoadLevel>();

        lastFrameLevelIndex = getNumberUI.GetIndex();
	}
	

	void Update () {
        if(lastFrameLevelIndex != getNumberUI.GetIndex())
        {
            CanRemove[] gameObjects = GameObject.FindObjectsOfType<CanRemove>();
            foreach(CanRemove gameObj in gameObjects)
            {
                Destroy(gameObj.gameObject);
            }
            levelLoader.Load(getNumberUI.GetIndex());
        }
        lastFrameLevelIndex = getNumberUI.GetIndex();
	}
}
