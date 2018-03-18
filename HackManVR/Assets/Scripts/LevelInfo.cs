using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo {

    public string levelName;
    public int objCount = 0;
    public List<MapTypes.Spawn> gameObjects = new List<MapTypes.Spawn>();
    public List<float> xPositions = new List<float>();
    public List<float> zPositions = new List<float>();


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
                case "BigPoint":
                    spawn = MapTypes.Spawn.Bigpoint;
                    break;
                
            }

            gameObjects.Add(spawn);
            xPositions.Add(gameObject.transform.position.x);
            zPositions.Add(gameObject.transform.position.z);
            objCount++;


            Debug.Log("Adding to levelinfo " + spawn + " at " + gameObject.transform.position);
        }
    }
    
}
