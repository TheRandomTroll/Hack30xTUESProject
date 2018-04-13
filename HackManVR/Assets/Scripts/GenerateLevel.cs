using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour {
    
    public Dictionary<Vector2Int, MapTypes.Spawn> levelDict = new Dictionary<Vector2Int, MapTypes.Spawn>();
    private WorldGrid worldGrid;

    [SerializeField] private bool readyForPlay = false;
    [SerializeField]
    GameObject wallPrefab, groundPrefab, cherryPrefab, portalPrefab, pacmanPrefab, pointPrefab, bigpointPrefab;

    [SerializeField]
    GameObject[] ghosts;

    [SerializeField] private NavigationMesh navMesh; // TODO: Remove SerializeField;
    private int currentGhostIndex;
    private int portalIndex = 0;

    void Start()
    {
        navMesh = FindObjectOfType<NavigationMesh>();
        worldGrid = FindObjectOfType<WorldGrid>();
    }

    public void LoadLevelInfo()
    {
        foreach (Vector2Int position in levelDict.Keys)
        {
            MapTypes.Spawn value = levelDict[position];
            Debug.Log("Generating: " + value);
            if (value == MapTypes.Spawn.Cube) // Wall
            {
                InstantiateObject(position, wallPrefab);
            }
            else if (value == MapTypes.Spawn.Point) // Path
            {
                GameObject point = InstantiateObject(position, pointPrefab);
                GameObject ground = InstantiateObject(position, groundPrefab);
                ConnectGameObject(point, ground);
            }
            else if (value == MapTypes.Spawn.Cherry)
            {
                GameObject cherry = InstantiateObject(position, cherryPrefab);
                GameObject ground = InstantiateObject(position, groundPrefab);
                ConnectGameObject(cherry, ground);
            }
            else if (value == MapTypes.Spawn.Portal)
            {
                GameObject port = InstantiateObject(position, portalPrefab);
                port.name = "PORTAL" + portalIndex;
                portalIndex++;
                GameObject ground = InstantiateObject(position, groundPrefab);
                ConnectGameObject(port, ground);
                
            }
            else if (value == (MapTypes.Spawn.Pac))
            {
                GameObject pacman = InstantiateObject(position, pacmanPrefab);
                GameObject ground = InstantiateObject(position, groundPrefab);
                ConnectGameObject(pacman, ground);
            }
            else if (value == MapTypes.Spawn.Ghost)
            {
                if (currentGhostIndex < ghosts.Length) {
                    GameObject ghost = InstantiateObject(position, ghosts[currentGhostIndex]);
                    GameObject ground = InstantiateObject(position, groundPrefab);
                    ConnectGameObject(ghost, ground);
                    currentGhostIndex++;
                }
            }
            else if(value == MapTypes.Spawn.Ground)
            {
                InstantiateObject(position, groundPrefab);
            }
            else if(value == MapTypes.Spawn.Bigpoint)
            {
                GameObject point = InstantiateObject(position, bigpointPrefab);
                GameObject ground = InstantiateObject(position, groundPrefab);
                ConnectGameObject(point, ground);
            }
        }
        if(readyForPlay)
        {
            navMesh.BuildNavMesh();
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

    private void ConnectGameObject(GameObject gameObjectOne, GameObject gameObjectTwo)
    {
        gameObjectOne.AddComponent<ConnectedTo>();
        gameObjectOne.GetComponent<ConnectedTo>().connection = gameObjectTwo;
        gameObjectTwo.AddComponent<ConnectedTo>();
        gameObjectTwo.GetComponent<ConnectedTo>().connection = gameObjectOne;
    }
}
