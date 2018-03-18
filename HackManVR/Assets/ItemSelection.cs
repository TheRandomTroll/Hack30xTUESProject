using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour {

    // TODO: After changing model redo script for simplicity. ~Krasi

    [SerializeField]
    private GameObject selectedPrefab;
    [SerializeField]
    private Color activeColor;

    [SerializeField]
    private float scaleBy = 1f;

    private Color normalColor = Color.white;

    [SerializeField]
    public bool oneTapToPlace = false;

    private void Start()
    {
        GameObject instance = Instantiate(selectedPrefab);
        instance.transform.position = transform.position;
        instance.transform.parent = transform;
        instance.transform.localScale *= scaleBy;
    }


    public void SetAsActive()
    {
        foreach (MeshRenderer renderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (!renderer.gameObject.GetComponent<Waypoint>())
            {
                renderer.material.color = activeColor;
            }
        }

    }


    public void Deactivate()
    {
        foreach(MeshRenderer renderer in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (!renderer.gameObject.GetComponent<Waypoint>() )
            {
                renderer.material.color = normalColor;
            }
        }
    }


    public GameObject GetPrefab()
    {
        return selectedPrefab;
    }


}
