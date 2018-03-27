using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SharedRaycast))]
public class ObjectPlacer : MonoBehaviour {


    [SerializeField] GameObject gridOutline;
    [SerializeField] GameObject removeOutline;
    [SerializeField] GameObject ground;

    [SerializeField] private string controllerLeft = "Left";
    [SerializeField] private string controllerRight = "Right";


    [SerializeField] GameObject currentSelectedObject;

    private WorldGrid worldGrid;
    private Vector2Int lastGridPosition;
    
    private bool oneTapToPlace = false;

    // Keys for debugging.
    private KeyCode leftClick = KeyCode.Mouse0;
    private KeyCode rightClick = KeyCode.Mouse1;

    private ObjectPlacer objectPlacer;
    private GameObject instantiatedShadow;


    private SharedRaycast raycast;


    private void Start()
    {
        worldGrid = FindObjectOfType<WorldGrid>();
        lastGridPosition = Vector2Int.zero;
        ChangeCurrentSelectedObject(currentSelectedObject);
        raycast = GetComponent<SharedRaycast>();
    }


    // TODO: Rework so that it doesn't instantiate a shadow of the object every time. 
    // Instead it moves the instantiated one!


    void Update () {
        if(!raycast.InfoIsRelevant()) {
            Destroy(instantiatedShadow);
            return;
        }

        RaycastHit hit = raycast.GetRaycastInfo();
        Vector2Int position = new Vector2Int(
                Mathf.RoundToInt(hit.transform.position.x),
                Mathf.RoundToInt(hit.transform.position.z));
        
            
        if (hit.transform.gameObject.tag == "BuildPoint")
        {
            if (position != lastGridPosition)
            {
                PlaceBuildShadow(position);
            }
            PlaceObject(hit, position);
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
            if(position != lastGridPosition)
            {
                PlaceRemoveShadow(position);
            }
            if (Input.GetKey(rightClick) || Input.GetButton("Right"))
            {
                RemoveItem(hit, position);
            }
        }

        lastGridPosition = position;
    }


    private void PlaceBuildShadow(Vector2Int position)
    {
        Shader shader = Shader.Find("Transparent/Diffuse");
        Material material = new Material(shader);
        Color color = currentSelectedObject.GetComponent<Renderer>().sharedMaterial.color;
        material.color = new Color(color.r, color.g, color.b, 0.75f);
        InstantiateObjectShadow(position, currentSelectedObject, material);
    }


    private void PlaceRemoveShadow(Vector2Int position)
    {
        Shader shader = Shader.Find("Transparent/Diffuse");
        Material material = new Material(shader);
        Color color = removeOutline.GetComponent<Renderer>().sharedMaterial.color;
        material.color = new Color(color.r, color.g, color.b, 0.75f);
        InstantiateObjectShadow(position, removeOutline, material);
    }


    private void RemoveItem(RaycastHit hit, Vector2Int removePos)
    {
        worldGrid.RemoveAt(removePos);
        Destroy(hit.transform.gameObject);

        GameObject grid = Instantiate(gridOutline);
        grid.transform.position = new Vector3(removePos.x, grid.transform.position.y, removePos.y);

        Destroy(instantiatedShadow);
    }

    private void PlaceObject(RaycastHit hit, Vector2Int position)
    {
        bool place = false;
        if (!oneTapToPlace && (Input.GetButton(controllerLeft) || Input.GetKey(leftClick)))
            place = true;
        if (oneTapToPlace && (Input.GetButtonDown(controllerLeft) || Input.GetKeyDown(leftClick)))
            place = true;

        if (place)
        {
            Debug.Log("Trying to place object!");
            if (!worldGrid.GridContainsAt(position))
            {
                GameObject gameObj = Instantiate(currentSelectedObject);
                gameObj.AddComponent<CanRemove>();
                Vector3 gameObjPos = hit.transform.position;
                gameObjPos.y = gameObj.transform.position.y;
                gameObj.transform.position = gameObjPos;
                worldGrid.AddToGrid(position, gameObj);
                Destroy(hit.transform.gameObject);

                Destroy(instantiatedShadow);

                // Instantiate ground
                if (currentSelectedObject.tag != "Wall" && currentSelectedObject.tag != "Ground")
                {
                    GameObject grnd = Instantiate(ground);
                    grnd.transform.position = new Vector3(
                        position.x,
                        grnd.transform.position.y,
                        position.y
                        );
                    grnd.AddComponent<CanRemove>();
                }
                PlaceRemoveShadow(position);
            }
            else
            {
                Debug.LogWarning("Grid contains something at " + position);
            }
        }
    }



    private void GetActiveItemFramePrefab(RaycastHit hit)
    {
        ItemSelection itemSelect = hit.transform.GetComponent<ItemSelection>();
        oneTapToPlace = itemSelect.GetOneTapToPlace();

        foreach (ItemSelection itemSel in FindObjectsOfType<ItemSelection>())
        {
            itemSel.Deactivate();
        }
        itemSelect.SetAsActive();
        ChangeCurrentSelectedObject(itemSelect.GetPrefab());
    }


    public void ChangeCurrentSelectedObject(GameObject gameObj)
    {
        currentSelectedObject = gameObj;
    }


    public GameObject GetCurrentSelectedObject()
    {
        return currentSelectedObject;
    }


    private void InstantiateObjectShadow(Vector2Int position, GameObject gameObject, Material material)
    {
        if (instantiatedShadow)
        {
            Destroy(instantiatedShadow);
        }

        instantiatedShadow = Instantiate(gameObject);
        instantiatedShadow.transform.position = new Vector3(
            position.x,
            gameObject.transform.position.y,
            position.y
        );
        instantiatedShadow.layer = 2; // Ignore Raycast layer;

        instantiatedShadow.GetComponent<MeshRenderer>().material = material;
    }
}
