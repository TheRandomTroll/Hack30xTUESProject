using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour {
    
    // X, Y are X and Z positions. Z is Y Rotation.
    public Dictionary<Vector3, MapTypes.Spawn> levelDict = new Dictionary<Vector3, MapTypes.Spawn>();
    private WorldGrid worldGrid;

    [SerializeField] private bool readyForPlay = false;
    [SerializeField]
    GameObject wallPrefab, groundPrefab, cherryPrefab, portalPrefab, pacmanPrefab, pointPrefab, bigpointPrefab;

    [SerializeField]
    GameObject[] ghosts;

    [SerializeField] private NavigationMesh navMesh; // TODO: Remove SerializeField;
    private int currentGhostIndex;

    [SerializeField] [Tooltip("For ground and wall only")]
    private int scaleValue = 1;

    private GameObject environment;

    void Start()
    {
        navMesh = FindObjectOfType<NavigationMesh>();
        worldGrid = FindObjectOfType<WorldGrid>();
        environment = new GameObject("Environment");

    }

    public void LoadLevelInfo()
    {
        currentGhostIndex = 0;
        foreach (Vector3 position in levelDict.Keys)
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

    public GameObject InstantiateObject(Vector3 positionRotation, GameObject toInstantiate)
    {
        if (!toInstantiate) return null;
        GameObject gameObj = Instantiate(toInstantiate);
        
        
        gameObj.AddComponent<CanRemove>(); // Don't remove!

        Vector2Int position = new Vector2Int((int) positionRotation.x, (int) positionRotation.y);
        if (gameObj.tag == "Wall" || gameObj.tag == "Ground")
        {
            gameObj.transform.localScale *= scaleValue;
        }
        position *= scaleValue;
        if (worldGrid) worldGrid.AddToGrid(position, gameObj);
        gameObj.transform.position = new Vector3(position.x, gameObj.transform.position.y, position.y);

        Vector3 rotation = gameObj.transform.eulerAngles;
        rotation.y = positionRotation.z;
        gameObj.transform.eulerAngles = rotation;
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
