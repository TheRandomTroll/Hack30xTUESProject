using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Linq;

public class SaveLevel {

    public static LevelInfo loadedLevel;


    public static void SerializeLevel(WorldGrid worldGrid, int levelIndex)
    {
        Dictionary<Vector2Int, GameObject> grid = worldGrid.GetGrid();

        List<GameObject> gameObjects = grid.Values.ToList();
        LevelInfo levelInfo = new LevelInfo(
            "Level " + levelIndex,
            gameObjects
            );


        foreach (GameObject gameObj in gameObjects) {
            Debug.Log("added " + gameObj.name + " to levelinfo");
        }
        

        Debug.LogWarning("Saving " + "/savedLevel" + levelIndex + ".level");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/savedLevel" +  levelIndex + ".level", FileMode.Create);
        bf.Serialize(file, levelInfo);
        Debug.LogWarning("Saved " + "/savedLevel" + levelIndex + ".level");
        file.Close();
    }


    public static void Load(int levelIndex)
    {
        if (File.Exists(Application.persistentDataPath + "/savedLevel" +  levelIndex + ".level"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedLevel" + levelIndex + ".level", FileMode.Open);
            loadedLevel = bf.Deserialize(file) as LevelInfo;
            file.Close();
        }
    }
}
