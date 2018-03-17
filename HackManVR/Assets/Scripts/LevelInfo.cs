using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo {

    public string levelName;
    public List<ObjectSaveData> gameObjects = new List<ObjectSaveData>();


    public LevelInfo(string levelName, List<GameObject> prefabs)
    {
        this.levelName = levelName;
        foreach(GameObject gameObject in prefabs)
        {
            // Make tags for each gameObject type and change to MapTypes.Spawn type;
            MapTypes.Spawn spawn = MapTypes.Spawn.Empty;
            switch(gameObject.tag)
            {
                case "Player":
                    spawn = MapTypes.Spawn.Pac;
                    break;
                case "Ghost":
                    spawn = MapTypes.Spawn.Ghost;
                    break;
                case "Cherry":
                    spawn = MapTypes.Spawn.Cherry;
                    break;
                case "Point":
                    spawn = MapTypes.Spawn.Point;
                    break;
                case "Portal":
                    spawn = MapTypes.Spawn.Portal;
                    break;
                case "Wall":
                    spawn = MapTypes.Spawn.Cube;
                    break;
                case "Ground":
                    spawn = MapTypes.Spawn.Ground;
                    break;
                
            }

            ObjectSaveData saveData = new ObjectSaveData(spawn, gameObject.transform.position);
            gameObjects.Add(saveData);
            Debug.Log("Adding to levelinfo " + spawn + " at " + gameObject.transform.position);
        }
    }
    
}
