using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour {

    HasTriggered trigger;


    // TODO: Change for console.
    KeyCode main = KeyCode.Mouse0;

    [SerializeField]
    string alternativeInput = "Backward";

    [SerializeField] // TODO: Remove
    GenerateLevel generator;

    [SerializeField]
    GetNumber getNumber;

    void Start () {
        trigger = GetComponent<HasTriggered>();
        generator = FindObjectOfType<GenerateLevel>();
	}
	

	void Update () {
        if (trigger.GetTrigger() && (Input.GetKeyDown(main) || Input.GetButtonDown(alternativeInput))) {
            SaveLevel.Load(getNumber.GetIndex());
            LevelInfo chosenLevel = SaveLevel.loadedLevel;
            if(chosenLevel == null)
            {
                Debug.LogError("Couldnt load level at save position: " + getNumber.GetIndex());
                return;
            }

            Dictionary<Vector2Int, MapTypes.Spawn> levelDict = new Dictionary<Vector2Int, MapTypes.Spawn>();


            for(int i = 0; i < chosenLevel.objCount; i++)
            {
                levelDict.Add(new Vector2Int(
                    Mathf.RoundToInt(chosenLevel.xPositions[i]), 
                    Mathf.RoundToInt(chosenLevel.zPositions[i])), 
                    chosenLevel.gameObjects[i]);
            }

            generator.levelDict = levelDict;
            generator.LoadLevelInfo();
            Debug.Log("Finished loading.");
        }
    }
}
