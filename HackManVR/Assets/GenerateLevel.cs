using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour {

    [SerializeField]
    int width, height; // Get from random map generator

    public Dictionary<Vector2Int, MapTypes.Spawn> levelDict = new Dictionary<Vector2Int, MapTypes.Spawn>();

    [SerializeField]
    GameObject wall, ground, cherry, portal, pacman, point;

    [SerializeField]
    GameObject[] ghosts;
    private int currentGhostIndex;

    public void LoadLevelInfo()
    {
        foreach (Vector2Int position in levelDict.Keys)
        {
            MapTypes.Spawn value = levelDict[position];
            Debug.Log("Generating: " + value);
            if (value == MapTypes.Spawn.Cube) // Wall
            {
                InstantiateObject(position, wall);
            }
            else if (value == MapTypes.Spawn.Point) // Path
            {
                InstantiateObject(position, point);
                InstantiateObject(position, ground);
            }
            else if (value == MapTypes.Spawn.Cherry)
            {
                InstantiateObject(position, cherry);
                InstantiateObject(position, ground);
            }
            else if (value == MapTypes.Spawn.Portal)
            {
                InstantiateObject(position, portal);
                InstantiateObject(position, ground);
            }
            else if (value == (MapTypes.Spawn.Pac))
            {
                InstantiateObject(position, pacman);
                InstantiateObject(position, ground);
            }
            else if (value == MapTypes.Spawn.Ghost)
            {
                if (currentGhostIndex < ghosts.Length) {
                    InstantiateObject(position, ghosts[currentGhostIndex]);
                    InstantiateObject(position, ground);
                    currentGhostIndex++;
                }
            }
            else if(value == MapTypes.Spawn.Ground)
            {
                InstantiateObject(position, ground);
            }
        }
    }

    public void InstantiateObject(Vector2Int position, GameObject toInstantiate)
    {
        if (!toInstantiate) return;
        GameObject gameObj = Instantiate(toInstantiate);
        gameObj.transform.position = new Vector3(position.x, gameObj.transform.position.y, position.y);

    }
}
