using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour {
    
    public Dictionary<Vector2Int, MapTypes.Spawn> levelDict = new Dictionary<Vector2Int, MapTypes.Spawn>();
    private WorldGrid worldGrid;

    [SerializeField]
    GameObject wall, ground, cherry, portal, pacman, point, bigpoint;

    [SerializeField]
    GameObject[] ghosts;
    private int currentGhostIndex;
    private int portalIndex = 0;

    void Start()
    {
        worldGrid = FindObjectOfType<WorldGrid>();
    }

    public void LoadLevelInfo()
    {
        worldGrid.Clear();
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
                GameObject port = InstantiateObject(position, portal);
                port.name = "PORTAL" + portalIndex;
                portalIndex++;
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
            else if(value == MapTypes.Spawn.Bigpoint)
            {
                InstantiateObject(position, bigpoint);
                InstantiateObject(position, ground);
            }
        }
    }

    public GameObject InstantiateObject(Vector2Int position, GameObject toInstantiate)
    {
        if (!toInstantiate) return null;
        GameObject gameObj = Instantiate(toInstantiate);
        
        gameObj.AddComponent<CanRemove>(); // Todo: Only do if in level edit
        if(worldGrid) worldGrid.AddToGrid(position, gameObj);
        gameObj.transform.position = new Vector3(position.x, gameObj.transform.position.y, position.y);
        
        return gameObj;
    }
}
