using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InitializeUIHitbox : MonoBehaviour {

    private BoxCollider boxCollider;
    private TextMesh textMesh;

	void Start () {
        bool isInDebugMode = Debug.isDebugBuild;
        if (!isInDebugMode) Destroy(this);

        textMesh = GetComponent<TextMesh>();
        if (!textMesh) textMesh = gameObject.AddComponent<TextMesh>();
        
        if(Application.isPlaying)
        {
            Destroy(this);
        }
    }


    void Update()
    {
        UpdateHitbox();
    }


    void UpdateHitbox()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider) DestroyImmediate(boxCollider);
        boxCollider = gameObject.AddComponent<BoxCollider>();
        Vector3 size = boxCollider.size;
        size.z = 10;
        boxCollider.size = size;
    }
}
