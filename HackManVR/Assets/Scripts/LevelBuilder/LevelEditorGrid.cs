using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGrid : MonoBehaviour {

    [SerializeField]
    int width = 10;
    [SerializeField]
    int height = 10;
    [SerializeField]
    GameObject gridOutline;


    private Dictionary<Vector2Int, GameObject> gridDict;

	void Start () {
        gridDict = new Dictionary<Vector2Int, GameObject>();
		for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                print("Adding grid to " + gridPosition);
                AddToGrid(gridPosition);
            }
        }
	}

    // Y never matters. Just adding for easy use.
    public void AddToGrid(Vector3 position)
    {
        AddToGrid(new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)));
    }


    public void AddToGrid(Vector2Int gridPosition)
    {
        if (!gridDict.ContainsKey(gridPosition))
        {
            GameObject grid = Instantiate(gridOutline);
            Vector3 position = new Vector3(gridPosition.x, grid.transform.position.y, gridPosition.y);
            grid.transform.position = position;
            gridDict.Add(gridPosition, grid);
        }
        else
        {
            Debug.LogWarning("Already has grid at " + gridPosition);
        }
    }

    


    public void RemoveFromGrid(Vector3 position)
    {
        RemoveFromGrid(new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)));
    }


    public void RemoveFromGrid(Vector2Int position)
    {
        if (gridDict.ContainsKey(position))
        {
            Destroy(gridDict[position]);
            gridDict.Remove(position);
        }
        else
        {
            Debug.LogWarning("No grid at " + position);
        }
    }
}
