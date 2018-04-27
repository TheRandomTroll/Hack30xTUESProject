using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScore : MonoBehaviour {

    TextMesh text;

	void Start () {
        SetPosition();
        text = GetComponent<TextMesh>();
	}

    private void SetPosition()
    {
        GroundMap map = FindObjectOfType<GroundMap>();
        // public Vector4 gridBounds = Vector4.zero; // (minX, minY, maxX, maxY)
        float centerX = (map.gridBounds.x + map.gridBounds.z) / 2;
        float centerZ = (map.gridBounds.y + map.gridBounds.w) / 2;
        Vector3 mapCenter = new Vector3(
            centerX,
                10,
            centerZ);

        transform.position = mapCenter;
        transform.Translate(-transform.forward * centerX);
    }

    public void UpdateScore(int score)
    {
        text.text = "Score: " + score;
    }
}
