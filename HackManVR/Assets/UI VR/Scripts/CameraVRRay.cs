﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SharedRaycast))]
public class CameraVRRay : MonoBehaviour {

    [SerializeField]
    GameObject point;

    private SharedRaycast raycast;


    private void Start()
    {
        point = Instantiate(point);
        point.GetComponent<MeshRenderer>().enabled = false;
        raycast = GetComponent<SharedRaycast>();
    }


    void Update () {
        if (!raycast.InfoIsRelevant()) {
            point.GetComponent<MeshRenderer>().enabled = false;
            return;
        }
        RaycastHit hit = raycast.GetRaycastInfo();
        if (!hit.transform) return;

        UITrigger isTriggered = hit.transform.GetComponent<UITrigger>();
        if (!isTriggered) return;
            

        isTriggered.Trigger();
        point.GetComponent<MeshRenderer>().enabled = true;
        point.transform.position = hit.point;
	}
}
