using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour {

    [SerializeField]
    GameObject currentSelectedObject;
    [SerializeField]
    private int rayDistance = 40;

    WorldGrid worldGrid;
    
    [SerializeField]
    GameObject transparentMarker;
    Queue<GameObject> deleteQueue = new Queue<GameObject>();

    [SerializeField]
    GameObject instantiatedShadow;
    [SerializeField]
    Vector2Int lastGridPosition;

    [SerializeField]
    GameObject gridOutline;


    // TODO: Change for controller.
    private KeyCode leftClick = KeyCode.Mouse0;
    private KeyCode rightClick = KeyCode.Mouse1;


    private void Start()
    {
        worldGrid = FindObjectOfType<WorldGrid>();
        lastGridPosition = new Vector2Int(0, 0);
        ChangeCurrentSelectedObject(currentSelectedObject);
        instantiatedShadow = Instantiate(transparentMarker);
    }


    void Update () {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.yellow);
        
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Vector2Int position = new Vector2Int(
                    Mathf.RoundToInt(hit.transform.position.x),
                    Mathf.RoundToInt(hit.transform.position.z));

            if (hit.transform.gameObject.tag == "BuildPoint")
            {
                if (position != lastGridPosition)
                {
                    InstantiateObjectShadow(position);
                }
                lastGridPosition = position;

                // TODO: Change getkey to controller btn.
                if (Input.GetKey(leftClick) || Input.GetButton("Left"))
                {
                    if (!worldGrid.GridContainsAt(position))
                    {
                        GameObject gameObj = Instantiate(currentSelectedObject);
                        Vector3 gameObjPos = hit.transform.position;
                        gameObjPos.y = gameObj.transform.position.y;
                        gameObj.transform.position = gameObjPos;
                        worldGrid.AddToGrid(position, gameObj);
                        Destroy(instantiatedShadow);
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("Grid contains something at " + position);
                    }
                }
            }

            else if(hit.transform.gameObject.tag == "ItemFrame")
            {
                if (Input.GetKey(leftClick) || Input.GetButton("Left"))
                {
                    GetActiveItemFramePrefab(hit);
                }
            }
            else if(hit.transform.GetComponent<Waypoint>())
            {
                if (Input.GetKey(rightClick) || Input.GetButton("Right"))
                {
                    Vector2Int removePos = new Vector2Int(
                        Mathf.RoundToInt(hit.transform.position.x),
                        Mathf.RoundToInt(hit.transform.position.z));

                    worldGrid.RemoveAt(removePos);
                    Destroy(hit.transform.gameObject);
                    GameObject grid = Instantiate(gridOutline);
                    grid.transform.position = new Vector3(removePos.x, grid.transform.position.y, removePos.y);
                }
            }
        }
	}

    private void InstantiateObjectShadow(Vector2Int position)
    {
        if (instantiatedShadow)
        {
            Destroy(instantiatedShadow);
        }
        instantiatedShadow = Instantiate(transparentMarker);
        instantiatedShadow.transform.position = new Vector3(
            position.x,
            transparentMarker.transform.position.y,
            position.y
        );

        instantiatedShadow.transform.localScale = new Vector3(
            instantiatedShadow.transform.lossyScale.x * 0.6f,
            instantiatedShadow.transform.lossyScale.y * 0.6f,
            instantiatedShadow.transform.lossyScale.z * 0.6f
        );

        MeshRenderer renderer = instantiatedShadow.GetComponent<MeshRenderer>();
        Color color = renderer.material.color;

        // TODO: Use transparent shader for marker.
        color.a = 50;
        color.b *= 1.2f;
        color.r *= 1.2f;
        color.g *= 1.2f;
        renderer.material.color = color;
    }


    private void GetActiveItemFramePrefab(RaycastHit hit)
    {
        ItemSelection itemSelect = hit.transform.GetComponent<ItemSelection>();
        foreach (ItemSelection itemSel in FindObjectsOfType<ItemSelection>())
        {
            itemSel.Deactivate();
        }
        itemSelect.SetAsActive();
        ChangeCurrentSelectedObject(itemSelect.GetPrefab());
    }


    public void ChangeCurrentSelectedObject(GameObject gameObj)
    {
        transparentMarker = gameObj;
        currentSelectedObject = gameObj;
    }
}
