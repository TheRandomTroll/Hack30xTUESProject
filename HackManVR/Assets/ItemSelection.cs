using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour {

    // TODO: After changing model redo script for simplicity. ~Krasi

    [SerializeField]
    private GameObject selectedPrefab;

    [SerializeField]
    private float scaleBy = 1f;

    [SerializeField]
    private Color activeColor;
    private Color normalColor = Color.white;

    [SerializeField]
    private bool oneTapToPlace = false;

    private void Start()
    {
        GameObject instance = Instantiate(selectedPrefab);
        instance.transform.position = transform.position;
        instance.transform.parent = transform;
        instance.transform.localScale *= scaleBy;
    }


    public void SetAsActive()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        if (!renderer.gameObject.GetComponent<Waypoint>())
        {
            renderer.material.color = activeColor;
        }
    }


    public void Deactivate()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        if (!renderer.gameObject.GetComponent<Waypoint>() )
        {
            renderer.material.color = normalColor;
        }
    }


    public GameObject GetPrefab()
    {
        return selectedPrefab;
    }


    public bool GetOneTapToPlace()
    {
        return oneTapToPlace;
    }


}
