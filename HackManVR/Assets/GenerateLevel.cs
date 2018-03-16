using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour {

    [SerializeField]
    int width, height; // Get from random map generator

    int[,] arr = new int[50, 50];


    [SerializeField]
    GameObject wall, ground, cherry, portal, pacman;

    [SerializeField]
    GameObject[] ghosts;

    private int currentGhostIndex;

	void Start () {
		for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int value = arr[x, y];
                GameObject gameObj = null;
                if (value == (int)MapTypes.Spawn.Cube) // Wall
                {
                    gameObj = Instantiate(wall);
                }
                else if(value == (int)MapTypes.Spawn.Point) // Path
                {
                    gameObj = Instantiate(ground);
                }
                else if(value == (int)MapTypes.Spawn.Cherry)
                {
                    gameObj = Instantiate(cherry);
                }
                else if(value == (int)MapTypes.Spawn.Portal)
                {
                    gameObj = Instantiate(portal);
                }
                else if(value == (int)MapTypes.Spawn.Pac)
                {
                    gameObj = Instantiate(pacman);
                }
                else if(value == (int)MapTypes.Spawn.Ghost)
                {
                    gameObj = Instantiate(ghosts[currentGhostIndex]);
                    currentGhostIndex++;
                }

                if (gameObj)
                {
                    gameObj.transform.position = new Vector3(x, gameObj.transform.position.y, y);
                }
                else
                {
                    Debug.LogWarning("Viktor e napravil debilno neshto s map gena!");
                }
            }
        }
	}
}
