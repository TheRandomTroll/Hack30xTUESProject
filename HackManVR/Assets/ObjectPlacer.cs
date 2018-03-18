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
    [SerializeField]
    GameObject ground;



    // TODO: Change for controller.
    private KeyCode leftClick = KeyCode.Mouse0;
    private KeyCode rightClick = KeyCode.Mouse1;

    [SerializeField]
    private bool oneTapToPlace = false;


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
                bool place = false;
                if (!oneTapToPlace && (Input.GetKey(leftClick) || Input.GetButton("Left"))) place = true;

                if (oneTapToPlace && (Input.GetKeyDown(leftClick) || Input.GetButtonDown("Left"))) place = true;

                if (place)
                {
                    if (!worldGrid.GridContainsAt(position))
                    {
                        GameObject gameObj = Instantiate(currentSelectedObject);
                        gameObj.AddComponent<CanRemove>();
                        Vector3 gameObjPos = hit.transform.position;
                        gameObjPos.y = gameObj.transform.position.y;
                        gameObj.transform.position = gameObjPos;
                        worldGrid.AddToGrid(position, gameObj);
                        Destroy(instantiatedShadow);
                        Destroy(hit.transform.gameObject);

                        // Instantiate ground
                        if (currentSelectedObject.tag != "Wall" && currentSelectedObject.tag != "Ground")
                        {
                            GameObject grnd = Instantiate(ground);
                            Vector3 groundPosition;
                            groundPosition = gameObjPos;
                            groundPosition.y = ground.transform.position.y;
                            grnd.transform.position = groundPosition;
                            grnd.AddComponent<CanRemove>();
                        }
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
            else if(hit.transform.GetComponent<CanRemove>())
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
        oneTapToPlace = itemSelect.oneTapToPlace;

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
