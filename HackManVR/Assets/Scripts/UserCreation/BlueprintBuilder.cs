using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintBuilder : MonoBehaviour
{
    public bool isBuilding;

    [Header("Used Objects")]
    public GameObject previewPrefab;
    private Transform previewObject;

    private float rotationCounter;
    private RaycastHit hit;
    private Vector3 facing = Vector3.up;

    public GameObject part;

    private Vector3 previousMousePosition;
    private Vector3 mousePosition;


    void Start()
    {
        SpawnPlacers();
    }

    void Update()
    {
        if (isBuilding)
        {
            MovePlacers();
            rotationCounter += Mathf.Round(Input.GetAxis("Mouse ScrollWheel") * 10);

            if (Input.GetKeyDown(KeyCode.R))
                rotationCounter++;

            RotatePlacers();
        }

        if (Input.GetMouseButtonDown(0))
        {

            PlaceObject();
        }

        if (Input.GetMouseButtonDown(1))
            RemoveObject();

        if (Input.GetMouseButtonDown(2))
        {
            if (hit.transform != null && hit.transform.gameObject.name != "Top")
            {
                previewPrefab = hit.transform.gameObject;

                previewPrefab.name = "cube";
                SpawnPlacers();
            }
        }
    }

    public void SpawnPlacers()
    {


        previewObject = SpawnPlacer(previewObject, previewPrefab);
        RotatePlacers();


        Renderer rend = previewObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Transparent/Diffuse");

        Color tempColor = rend.material.color;
        tempColor.a = 0.5F;
        rend.material.color = tempColor;

        /*rend2.material.shader = Shader.Find("Transparent/Diffuse");


        Color tempColor2 = rend2.material.color;
        tempColor2.a = 0F;
        rend2.material.color = tempColor;*/
    }

    private Transform SpawnPlacer(Transform placer, GameObject prefab)
    {
        Vector3 placerPosition = Vector3.zero;
        if (placer != null)
        {

            placerPosition = placer.position;
            Destroy(placer.gameObject);

        }


        GameObject placerObject = Instantiate(prefab, placerPosition, Quaternion.identity) as GameObject;
        placerObject.layer = 2;
        placerObject.tag = "NotPlaced";


        return placerObject.transform;
    }

    private void MovePlacers()
    {

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {

            if (hit.transform.gameObject.name == "Top")
            {

                facing = hit.normal; //Vector3.up
                previewObject.position = hit.point.Round();
            }
            else
            {
                if (hit.transform.GetComponent<ConnectionPoints>().IsConnectionPoint(hit.point))
                {
                    facing = hit.normal;
                    previewObject.position = (hit.point.SnapToGrid() + (facing / 2)).Round();
                }
                else
                {
                    //Turn the cubes invisible.
                }
            }
        }
        else
        {
            //Turn the cubes invisible.
        }
    }

    private void RotatePlacers()
    {
        previewObject.up = facing;

        previewObject.RotateAround(previewObject.position, previewObject.up, rotationCounter * 90);

    }

    private void PlaceObject()
    {
        if (previewObject.position.IsWithinBounds())
            part = Instantiate(previewPrefab, previewObject.position, previewObject.rotation, transform);

        part.GetComponent<Collider>().isTrigger = true;
        part.GetComponent<SphereCollider>().isTrigger = true;
    }

    private void RemoveObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).position == hit.transform.position)
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}