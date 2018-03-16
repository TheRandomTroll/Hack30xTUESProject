using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoints : MonoBehaviour
{
    [Header("Bottom point is added by default.")]
    public List<Points> points = new List<Points>();
    public bool isConnected;
    public bool isChecked;

    void Start()
    {
        points.Add(new Points(Vector3.down / 2));
        foreach (Points connection in points)
        {
            //Rounding it just to be sure, we dont want to accidentally get issues from our own or calculatory mistakes.
            connection.point = connection.point.SnapToGrid();
        }
    }

    public bool IsConnectionPoint(Vector3 hitPoint)
    {
        hitPoint = transform.InverseTransformPoint(hitPoint).SnapToGrid();

        Points[] connections = points.ToArray();
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].point == hitPoint)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class Points
{
    public Vector3 point;
    public Points(Vector3 V3)
    {
        point = V3;
    }
}