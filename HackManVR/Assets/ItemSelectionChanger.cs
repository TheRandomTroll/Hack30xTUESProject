using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemSelection))]
public class ItemSelectionChanger : MonoBehaviour {

    [SerializeField] GameObject alternativePrefab;
    [SerializeField] int mainPrefabMaxCount = 1;
    [SerializeField] bool alternativeOneTapToPlace = false;

    private ItemSelection itemSelection;
    private GameObject mainPrefab;

    private bool usingMainPrefab = true;
    private bool mainOneTap;

	void Start () {
        itemSelection = GetComponent<ItemSelection>();
        mainPrefab = itemSelection.GetSelection();
        mainOneTap = itemSelection.GetOneTapToPlace();
	}
	

	void Update () {
        int mainObjectCount = GameObject.FindGameObjectsWithTag(mainPrefab.tag).Length;
        if(!usingMainPrefab && mainObjectCount < mainPrefabMaxCount)
        {
            itemSelection.SetSelection(mainPrefab);
            itemSelection.SetOneTapToPlace(mainOneTap);
            usingMainPrefab = true;
        }
        else if(usingMainPrefab && mainObjectCount >= mainPrefabMaxCount)
        {
            itemSelection.SetSelection(alternativePrefab);
            itemSelection.SetOneTapToPlace(alternativeOneTapToPlace);
            usingMainPrefab = false;
        }
	}
}
