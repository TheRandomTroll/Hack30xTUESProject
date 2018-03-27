using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour {

    Dictionary<Vector2Int, GameObject> worldGrid = new Dictionary<Vector2Int, GameObject>();
    

    public void AddToGrid(Vector2Int position, GameObject gameObj)
    {
        if (!GridContainsAt(position))
        {
            worldGrid.Add(position, gameObj);
        }
    }


    public void Clear()
    {
        worldGrid.Clear();
    }

	
    public bool GridContainsAt(Vector2Int position)
    {
        return worldGrid.ContainsKey(position);
    }


    public void RemoveAt(Vector2Int position)
    {
        worldGrid.Remove(position);
        Debug.Log("Removed " + position);
    }


    public Dictionary<Vector2Int, GameObject> GetGrid()
    {
        return worldGrid;
    }
}
