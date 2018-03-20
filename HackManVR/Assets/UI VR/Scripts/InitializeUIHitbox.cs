using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InitializeUIHitbox : MonoBehaviour {

    [SerializeField]
    private bool executeInEditModeOnly = true;

    private BoxCollider boxCollider;
    private TextMesh textMesh;

	void Start () {
        bool isInEditMode = Debug.isDebugBuild;
        if (executeInEditModeOnly && !isInEditMode) Destroy(this);

        textMesh = GetComponent<TextMesh>();
        if (!textMesh) textMesh = gameObject.AddComponent<TextMesh>();
    }


    void Update()
    {
        UpdateHitbox();
    }


    void UpdateHitbox()
    {
        // Hitbox perfectly updated when adding a new one!.
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider) DestroyImmediate(boxCollider);
        boxCollider = gameObject.AddComponent<BoxCollider>();
    }
}
