using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private int gridSize;
    [SerializeField]
    List<KeyValuePair> gridMap = new List<KeyValuePair>(); // TODO: Change to dictionary.

    private Dictionary<Vector2Int, GameObject> gridDict = new Dictionary<Vector2Int, GameObject>();

    void Start () {
	    foreach(KeyValuePair keyValue in gridMap)
        {
            Vector2Int position = keyValue.position;
            GameObject prefab = keyValue.prefab;
            GameObject instance = Instantiate(prefab);
            instance.transform.position = new Vector3(
                position.x * gridSize, prefab.transform.position.y, position.y * gridSize);
        }	
	}
}
