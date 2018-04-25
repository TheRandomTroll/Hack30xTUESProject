using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadPlayLevel : MonoBehaviour {
    

	void Awake () {
        LoadLevelFromResources(ChosenLevel.levelIndex);
	}

    private static void LoadLevelFromResources(int index)
    {
        BinaryFormatter bf = new BinaryFormatter();
        TextAsset binData = Resources.Load("mainLevel" + index) as TextAsset;
        if (binData == null) Debug.LogError("Cant find!");
        MemoryStream binaryFile = new MemoryStream(binData.bytes);
        LevelInfo chosenLevel = bf.Deserialize(binaryFile) as LevelInfo;

        GenerateLevel levelGenerator = FindObjectOfType<GenerateLevel>();
        Dictionary<Vector3, MapTypes.Spawn> levelDict = new Dictionary<Vector3, MapTypes.Spawn>();
        for (int i = 0; i < chosenLevel.objCount; i++)
        {
            levelDict.Add(
                new Vector3(chosenLevel.xPositions[i], chosenLevel.zPositions[i], chosenLevel.yRotations[i]),
                chosenLevel.gameObjects[i]);
        }
        levelGenerator.levelDict = levelDict;
        levelGenerator.LoadLevelInfo();
    }
}
