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

    private GameObject instance;
    private void Start()
    {
        InstantiateCurrentPrefab();
    }


    private void InstantiateCurrentPrefab()
    {
        if(instance)
            Destroy(instance);

        instance = Instantiate(selectedPrefab);
        instance.tag = "Untagged";
        instance.transform.position = transform.position;
        instance.transform.parent = transform;
        instance.transform.localScale *= scaleBy;
    }

    public void SetAsActive()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material.color = activeColor;
    }


    public void Deactivate()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material.color = normalColor;
    }


    public GameObject GetSelection()
    {
        return selectedPrefab;
    }


    public void SetSelection(GameObject newPrefab)
    {
        selectedPrefab = newPrefab;
        InstantiateCurrentPrefab();
    }


    public bool GetOneTapToPlace()
    {
        return oneTapToPlace;
    }

    public void SetOneTapToPlace(bool oneTap)
    {
        oneTapToPlace = oneTap;
    }


}
