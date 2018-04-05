using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    [SerializeField] // TODO: Remove
    GenerateLevel generator;

    private WorldGrid worldGrid;
    private LevelEditorGrid levelGrid;
    void Start()
    {
        generator = FindObjectOfType<GenerateLevel>();
        worldGrid = FindObjectOfType<WorldGrid>();
        levelGrid = FindObjectOfType<LevelEditorGrid>();
    }


    public void Load(int index) {
        SaveLevel.Load(index);
        LevelInfo chosenLevel = SaveLevel.loadedLevel;
        if (chosenLevel == null)
        {
            Debug.LogError("Couldnt load level at save position: " + index);
            return;
        }

        Dictionary<Vector2Int, MapTypes.Spawn> levelDict = new Dictionary<Vector2Int, MapTypes.Spawn>();

        // Set both to starting state!
        if(worldGrid) worldGrid.Clear();
        if(levelGrid) levelGrid.GenerateStartingGrid();

        for (int i = 0; i < chosenLevel.objCount; i++)
        {
            Vector2Int gridPosition = new Vector2Int(
                Mathf.RoundToInt(chosenLevel.xPositions[i]),
                Mathf.RoundToInt(chosenLevel.zPositions[i]));
            if(levelGrid) levelGrid.RemoveFromGrid(gridPosition);
            levelDict.Add(gridPosition, chosenLevel.gameObjects[i]);
        }

        generator.levelDict = levelDict;
        generator.LoadLevelInfo();
        Debug.Log("Finished loading.");
    }
}
