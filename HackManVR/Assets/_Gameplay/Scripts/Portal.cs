using System;
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
        if(connectedPortal)
        {
            SimulatePlayerLookingThroughCamera(frontCamera, connectedPortal.backPlane);
            SimulatePlayerLookingThroughCamera(backCamera, connectedPortal.frontPlane);
        }
    }

    private void SimulatePlayerLookingThroughCamera(Camera camera, GameObject renderPlane)
    {
        /*
        Vector3 playerOffsetFromPortal = playerCamera.position - renderPlane.transform.position;
        camera.transform.position = renderPlane.transform.position;
        camera.transform.Translate(transform.position + playerOffsetFromPortal, camera.transform.parent);
        
        /*
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        */
    }

    private IEnumerator SetPortalConnections()
    {
        while (true)
        {
            var portals = FindObjectsOfType<Portal>();
            foreach (Portal portal in portals)
            {
                if (portal != this && portal.connectedPortal == null)
                {
                    SetupPortalConnection(portal);
                    yield break;
                }
            }
            if(destroyIfNoPortals)
            {
                Destroy(gameObject);
                break;
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }


    private void SetupPortalConnection(Portal portal)
    {
        connectedPortal = portal;
        portal.connectedPortal = this;
        SetCameraToRenderSurface(connectedPortal.frontCamera, backPlane);
        SetCameraToRenderSurface(connectedPortal.backCamera, frontPlane);
        SetCameraToRenderSurface(frontCamera, connectedPortal.backPlane);
        SetCameraToRenderSurface(backCamera, connectedPortal.frontPlane);
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
