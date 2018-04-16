using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenLevel : MonoBehaviour {
    public enum LevelLoadType {StreamingAssets, PersistentDataPath};

    public static int levelIndex = 0;
    public static LevelLoadType loadType;

}
