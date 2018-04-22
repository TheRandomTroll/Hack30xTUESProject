using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadLevelsFromResources : MonoBehaviour {
    
	void Start ()
    {
        LoadLevelFromResources(1);
    }

    private static void LoadLevelFromResources(int index)
    {
        BinaryFormatter bf = new BinaryFormatter();
        TextAsset binData = Resources.Load("savedLevel" + index) as TextAsset;
        if (binData == null) Debug.LogError("Cant find!");
        MemoryStream binaryFile = new MemoryStream(binData.bytes);
        LevelInfo chosenLevel = bf.Deserialize(binaryFile) as LevelInfo;

        Dictionary<Vector3, MapTypes.Spawn> levelDict = new Dictionary<Vector3, MapTypes.Spawn>();

        for (int i = 0; i < chosenLevel.objCount; i++)
        {
            Vector2Int gridPosition = new Vector2Int(
                Mathf.RoundToInt(chosenLevel.xPositions[i]),
                Mathf.RoundToInt(chosenLevel.zPositions[i]));
            levelDict.Add(
                new Vector3(chosenLevel.xPositions[i], chosenLevel.zPositions[i], chosenLevel.yRotations[i]),
                chosenLevel.gameObjects[i]);
        }

        FindObjectOfType<GenerateLevel>().levelDict = levelDict;
        FindObjectOfType<GenerateLevel>().LoadLevelInfo();
        Debug.Log("Finished loading.");
    }
}
