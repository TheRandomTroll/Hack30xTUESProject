using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCreatorGrid : MonoBehaviour {

    [SerializeField]
    int width = 10;
    [SerializeField]
    int height = 10;
    [SerializeField]
    GameObject gridOutline;


	void Start () {
		for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                GameObject gameObj = Instantiate(gridOutline);
                gameObj.transform.position = new Vector3(x, 0, y);
            }
        }
	}
}
