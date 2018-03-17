using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Linq;

public class SaveLevel {
    
    public static List<LevelInfo> levels = new List<LevelInfo>();
    public static int levelCount = 0;
    
    public static void SerializeLevel(WorldGrid worldGrid)
    {
        Dictionary<Vector2Int, GameObject> grid = worldGrid.GetGrid();

        List<GameObject> gameObjects = grid.Values.ToList();
        LevelInfo levelInfo = new LevelInfo(
            "Level " + levelCount,
            gameObjects
            );


        foreach (GameObject gameObj in gameObjects) {
            Debug.Log("added " + gameObj.name + " to levelinfo");
        }
        

        Debug.LogWarning("Saving " + levelInfo.levelName);
        levels.Add(levelInfo);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/savedLevels.level", FileMode.OpenOrCreate);
        bf.Serialize(file, SaveLevel.levels);
        Debug.LogWarning("Saved " + levelInfo.levelName);
        levelCount++;
        file.Close();
    }


    public static void Load()
    {
        if (File.Exists("/savedLevels.level"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/savedLevels.level", FileMode.Open);
            levels = (List<LevelInfo>)bf.Deserialize(file);
            levelCount = levels.Count;
            file.Close();
        }
    }
}
