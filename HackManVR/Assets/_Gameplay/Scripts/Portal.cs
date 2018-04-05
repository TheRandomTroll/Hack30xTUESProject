using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField] private Portal connectedPortal;

    [SerializeField] private float inactiveTime = 1f;
    private bool disabled = false;

    [SerializeField] public Camera frontCamera;
    [SerializeField] public Camera backCamera;

    [SerializeField] private GameObject frontPlane;
    [SerializeField] private GameObject backPlane;

    [SerializeField] private bool destroyIfNoPortals = false;
	void Start ()
    {
        StartCoroutine(SetPortalConnections());
    }

    private IEnumerator SetPortalConnections()
    {
        var portals = FindObjectsOfType<Portal>();
        if (portals.Length == 1) // Only this portal
        {
            Debug.LogWarning("hello");
            if (destroyIfNoPortals)
            {
                Destroy(gameObject);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }

        foreach (Portal portal in portals)
        {
            if (portal != this)
            {
                connectedPortal = portal;
                SetCameraToRenderSurface(connectedPortal.frontCamera, frontPlane);
                SetCameraToRenderSurface(connectedPortal.backCamera, backPlane);
                break;
            }
        }
    }

    private void SetCameraToRenderSurface(Camera targetCamera, GameObject targetSurface)
    {
        RenderTexture renderTexture = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();
        targetCamera.targetTexture = renderTexture;
        Shader shader = Shader.Find("Unlit/ScreenCutoutShader");
        Material material = new Material(shader);
        material.mainTexture = renderTexture;
        MeshRenderer renderer = targetSurface.GetComponent<MeshRenderer>();
        List<Material> materials = renderer.materials.ToList();
        materials.Insert(0, material); // Insert before transparent material
        renderer.materials = materials.ToArray();
    }



	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (disabled) return;

        if(other.tag == "Player")
        {
            Transform player = other.transform;
            if(player.forward == transform.forward || player.forward == -transform.forward)
            {
                player.position = connectedPortal.transform.position;
                connectedPortal.Disable();
            }
        }
    }


    public void Disable()
    {
        disabled = true;
        StartCoroutine(EnableAfter(inactiveTime));
    }

    private IEnumerator EnableAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        disabled = false;
    }
}
