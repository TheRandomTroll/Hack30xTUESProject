using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectSaveData {

    public float x;
    public float y;
    public float z;

    public MapTypes.Spawn spawnType;


    public ObjectSaveData(MapTypes.Spawn spawnType, Vector3 position)
    {
        this.spawnType = spawnType;
        x = position.x;
        y = position.y;
        z = position.z;
    }
}
