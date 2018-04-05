using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InitializeUIHitbox : MonoBehaviour {

    private BoxCollider boxCollider;
    private TextMesh textMesh;

	void Start () {

        textMesh = GetComponent<TextMesh>();
        if (!textMesh) textMesh = gameObject.AddComponent<TextMesh>();
        
    }


    void Update()
    {
        UpdateHitbox();
    }


    void UpdateHitbox()
    {
        BoxCollider[] boxColliders = GetComponents<BoxCollider>();
        foreach (BoxCollider collider in boxColliders)
            DestroyImmediate(collider);

        boxCollider = gameObject.AddComponent<BoxCollider>();
        Vector3 size = boxCollider.size;
        size.z = 10;
        boxCollider.size = size;
    }
}
