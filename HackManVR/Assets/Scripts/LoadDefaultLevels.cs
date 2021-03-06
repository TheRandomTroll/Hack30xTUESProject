﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadDefaultLevels : MonoBehaviour {

    [SerializeField] int defaultLevelCount = 3;

	void Start () {
        if (PlayerPrefs.GetInt("HasLoadedLevels", 0) != 1)
        {
            for (int i = 0; i < defaultLevelCount; i++)
            {
                LoadLevelFromResources(i);
                PlayerPrefs.SetInt("HasLoadedLevels", 1);
            }
        }
        else
        {
            Debug.LogWarning("Not Loading Default Levels!");
        }
	}

    private static void LoadLevelFromResources(int index)
    {
        BinaryFormatter bf = new BinaryFormatter();
        TextAsset binData = Resources.Load("savedLevel" + index) as TextAsset;
        if (binData == null) Debug.LogError("Cant find!");
        MemoryStream binaryFile = new MemoryStream(binData.bytes);
        LevelInfo chosenLevel = bf.Deserialize(binaryFile) as LevelInfo;

        FileStream file = File.Open(Application.persistentDataPath + "/savedLevel" + index + ".level", FileMode.Create);
        bf.Serialize(file, chosenLevel);
        Debug.LogWarning("Saved " + "/savedLevel" + index + ".level");
        file.Close();
    }
}
