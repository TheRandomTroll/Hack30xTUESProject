using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStatic : MonoBehaviour {

    [SerializeField] private float cameraHeight;
    private Camera staticCamera;

    private Vector3 position;
    private bool hasSetPosition = false;

	void Start () {
        //  transform.eulerAngles = new Vector3(0, 0, -90); // Za da gleda drugata kamera vpravo.
    }

    public void SetCameraPosition(Vector4 mapBounds)
    {
        staticCamera = GetComponentInChildren<Camera>();
        GroundMap map = FindObjectOfType<GroundMap>();
        float xCenter = (map.gridBounds.x + map.gridBounds.z);
        float zCenter = (map.gridBounds.y + map.gridBounds.w);
        cameraHeight = ((xCenter * 2 * zCenter * 2) / staticCamera.fieldOfView / 2); //
        cameraHeight -= cameraHeight * 0.15f;


        Debug.Log("Map bounds: " + mapBounds);
        Debug.Log("xCenter: " + xCenter);

        Debug.Log("zCenter: " + zCenter);

        position = new Vector3(xCenter, cameraHeight, zCenter);
        hasSetPosition = true;
    }

    private void LateUpdate()
    {
        if(hasSetPosition)
        {
            FindObjectOfType<CameraStatic>().transform.position = position;
        }
    }
}
