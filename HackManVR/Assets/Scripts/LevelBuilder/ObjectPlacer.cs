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

    // Choice to set in the beggining something as a default item selection.
    [SerializeField] private ItemSelection currentItemSelection;

    private WorldGrid worldGrid;
    private Vector3 lastPosition;

    // Keys for debugging.
    private KeyCode leftClick = KeyCode.Mouse0;
    private KeyCode rightClick = KeyCode.Mouse1;

    private ObjectPlacer objectPlacer;


    private GameObject instantiatedShadow;


    private SharedRaycast raycast;
    private string currentShadowName; // Can't find something else to differentiate shadows by.
    private LevelEditorGrid levelGrid;

    private void Start()
    {
        worldGrid = FindObjectOfType<WorldGrid>();
        lastPosition = Vector3.zero;
        if(currentItemSelection) currentItemSelection.SetAsActive();
        levelGrid = FindObjectOfType<LevelEditorGrid>();
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
        if (!hit.transform) return;
        Vector2Int gridPosition = new Vector2Int(
                Mathf.RoundToInt(hit.transform.position.x),
                Mathf.RoundToInt(hit.transform.position.z));
        
            
        if (hit.transform.gameObject.tag == "BuildPoint")
        {
            if (lastPosition != hit.transform.position)
            {
                PlaceBuildShadow(gridPosition);
            }
            PlaceObject(hit, gridPosition);
        }
        else if(hit.transform.gameObject.tag == "ItemFrame")
        {
            if (Input.GetKey(leftClick) || Input.GetButton("Left"))
            {
                SetActiveItemFrame(hit);
            }
        }
        else if(hit.transform.GetComponent<CanRemove>())
        {

            // TODO: Check not whether the position but the target has changed!
            if (hit.transform.position != lastPosition)
            {
                PlaceRemoveShadow(hit.transform.position);
            }
            if (Input.GetKey(rightClick) || Input.GetButton("Right"))
            {
                RemoveItem(hit, gridPosition);
            }
        }

        lastPosition = hit.transform.position;
    }


    private void PlaceBuildShadow(Vector2Int position)
    {
        Shader shader = Shader.Find("Transparent/Diffuse");
        Material material = new Material(shader);
        GameObject currentSelectedObject = currentItemSelection.GetSelection();
        Color color = currentSelectedObject.GetComponent<Renderer>().sharedMaterial.color;
        material.color = new Color(color.r, color.g, color.b, 0.75f);
        InstantiateObjectShadow(position, currentSelectedObject, material);
    }


    private void PlaceRemoveShadow(Vector3 position)
    {
        // TODO: Change y position dynamically!

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
        ConnectedTo connection = hit.transform.gameObject.GetComponent<ConnectedTo>();
        if (connection) Destroy(connection.connection);

        levelGrid.AddToGrid(new Vector2Int(removePos.x, removePos.y));

        Destroy(instantiatedShadow);
    }

    private void PlaceObject(RaycastHit hit, Vector2Int position)
    {
        bool oneTapToPlace = currentItemSelection.GetOneTapToPlace();
        bool place = false;
        if (!oneTapToPlace && (Input.GetButton(controllerLeft) || Input.GetKey(leftClick)))
            place = true;
        if (oneTapToPlace && (Input.GetButtonDown(controllerLeft) || Input.GetKeyDown(leftClick)))
            place = true;

        if (place)
        {
            if (!worldGrid.GridContainsAt(position))
            {
                GameObject gameObj = Instantiate(currentItemSelection.GetSelection());
                gameObj.AddComponent<CanRemove>();

                Vector3 gameObjPos = hit.transform.position;
                gameObjPos.y = gameObj.transform.position.y;
                gameObj.transform.position = gameObjPos;

                worldGrid.AddToGrid(position, gameObj);
                
                levelGrid.RemoveFromGrid(position);

                PlaceRemoveShadow(gameObj.transform.position);

                // Instantiate ground
                if (gameObj.tag != "Wall" && gameObj.tag != "Ground")
                {
                    GameObject grnd = Instantiate(ground);
                    grnd.transform.position = new Vector3(
                        position.x,
                        grnd.transform.position.y,
                        position.y
                        );
                    grnd.AddComponent<CanRemove>();

                    ConnectedTo groundConnection = grnd.AddComponent<ConnectedTo>();
                    groundConnection.connection = gameObj;
                    ConnectedTo gameObjectConnection = gameObj.AddComponent<ConnectedTo>();
                    gameObjectConnection.connection = grnd;
                }
            }
            else
            {
                Debug.LogWarning("Grid contains something at " + position);
            }
        }
    }



    private void SetActiveItemFrame(RaycastHit hit)
    {
        ItemSelection itemSelect = hit.transform.GetComponent<ItemSelection>();

        foreach (ItemSelection itemSel in FindObjectsOfType<ItemSelection>())
        {
            itemSel.Deactivate();
        }
        itemSelect.SetAsActive();
        currentItemSelection = itemSelect;

    }

    private void InstantiateObjectShadow(Vector2Int position, GameObject gameObject, Material material)
    {
        Vector3 pos = new Vector3(position.x, gameObject.transform.position.y, position.y);
        InstantiateObjectShadow(pos, gameObject, material);
    }


    private void InstantiateObjectShadow(Vector3 position, GameObject gameObject, Material material)
    {
        if (instantiatedShadow)
        {
            if (currentShadowName == gameObject.name)
            {
                instantiatedShadow.transform.position = position;
                return;
            }
            else
            {
                Destroy(instantiatedShadow);
            }
        }

        currentShadowName = gameObject.name;
        instantiatedShadow = Instantiate(gameObject);
        instantiatedShadow.tag = "Untagged"; // SO it doesn't show up in ItemSelectionChanger item count.
        instantiatedShadow.transform.position = new Vector3(
            position.x,
            position.y,
            position.z
        );
        instantiatedShadow.layer = 2; // Ignore Raycast layer;
        instantiatedShadow.GetComponent<MeshRenderer>().material = material;
    }
}
