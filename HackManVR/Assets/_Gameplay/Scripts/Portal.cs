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

    [SerializeField] private Transform playerCameraParent;
    [SerializeField] private Transform playerCamera;
	void Start ()
    {
        StartCoroutine(SetPortalConnections());
        playerCamera = Camera.main.transform;
        playerCameraParent = playerCamera.parent;
    }

    private void Update()
    {

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
                SetCameraToRenderSurface(connectedPortal.frontCamera, backPlane);
                SetCameraToRenderSurface(connectedPortal.backCamera, frontPlane);
                break;
            }
        }
    }

    private void SetCameraToRenderSurface(Camera targetCamera, GameObject targetSurface)
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
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


    private void OnTriggerEnter(Collider other)
    {
        if (disabled) return;

        if(other.tag == "Player")
        {
            Transform player = other.transform;
            if (player.forward == transform.forward || player.forward == -transform.forward)
            {
                connectedPortal.Disable();
                Vector3 target;
                if (transform.forward == player.forward)
                    target = connectedPortal.transform.forward + connectedPortal.transform.position;
                else
                    target = -connectedPortal.transform.forward + connectedPortal.transform.position;
                player.transform.position = connectedPortal.transform.position; // Only for LookAt
                player.transform.LookAt(target);
                player.transform.position = target;

                // Keep direction that camera had before teleporting.
                Vector3 portalDifference = connectedPortal.transform.rotation.eulerAngles - transform.rotation.eulerAngles;
                Vector3 compensateRotation = playerCameraParent.eulerAngles + portalDifference;
                playerCameraParent.eulerAngles = compensateRotation;
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
