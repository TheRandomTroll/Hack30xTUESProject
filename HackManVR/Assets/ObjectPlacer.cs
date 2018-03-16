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
                        transform.localScale.x * 0.6f,
                        transform.localScale.y * 0.6f,
                        transform.localScale.z * 0.6f
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
                lastGridPosition = position;

                // TODO: Change getkey to controller btn.
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (!worldGrid.GridContainsAt(position))
                    {
                        GameObject gameObj = Instantiate(currentSelectedObject);
                        gameObj.transform.position = hit.transform.position;
                        worldGrid.AddToGrid(position, gameObj);
                        Destroy(instantiatedShadow);
                    }
                    else
                    {
                        Debug.LogWarning("Grid contains something at " + position);
                    }
                }
            }

            if(hit.transform.gameObject.tag == "ItemFrame")
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    GetActiveItemFramePrefab(hit);
                }
            }
        }
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
