using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedRaycast : MonoBehaviour {

    [SerializeField] private float distance = 50;


    private RaycastHit raycastInfo;
    private bool hasInfo = false;
    
	void FixedUpdate () {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.yellow);
        if (Physics.Raycast(ray, out raycastInfo, distance))
        {
            hasInfo = true;
        }
        else
        {
            hasInfo = false;
        }
    }
    
    public bool InfoIsRelevant()
    {
        return hasInfo;
    }


    public RaycastHit GetRaycastInfo()
    {
        return raycastInfo;
    }
}
