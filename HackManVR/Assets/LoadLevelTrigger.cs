using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour {

    HasTriggered trigger;


    // TODO: Change for console.
    KeyCode main = KeyCode.Mouse0;

    [SerializeField]
    string alternativeInput = "Backward";

    [SerializeField]
    private int levelIndex = 0;

    [SerializeField] // TODO: Remove
    GenerateLevel generator;

    void Start () {
        trigger = GetComponent<HasTriggered>();
        generator = FindObjectOfType<GenerateLevel>();
	}
	

	void Update () {
        if (trigger.GetTrigger() && (Input.GetKey(main) || Input.GetButtonDown(alternativeInput))) {
            SaveLevel.Load();
            List<LevelInfo> levels = SaveLevel.levels;
            LevelInfo chosenLevel = levels[levelIndex];
            Debug.Log("Loading: " + chosenLevel.levelName);
            Dictionary<Vector2Int, MapTypes.Spawn> levelDict = new Dictionary<Vector2Int, MapTypes.Spawn>();
            foreach(ObjectSaveData saveData in chosenLevel.gameObjects)
            {
                Vector3 position = new Vector3(saveData.x, saveData.y, saveData.z);
                Debug.Log("Loading " + saveData.spawnType + " at " + position);
                levelDict.Add(new Vector2Int(
                    Mathf.RoundToInt(saveData.x), 
                    Mathf.RoundToInt(saveData.z)), 
                    saveData.spawnType);
            }

            generator.levelDict = levelDict;
            generator.LoadLevelInfo();
            Debug.Log("Finished loading.");
            Destroy(this);
        }
    }
}
