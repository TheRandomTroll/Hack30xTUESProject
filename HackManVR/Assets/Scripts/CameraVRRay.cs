using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVRRay : MonoBehaviour {

    [SerializeField]
    GameObject point;
    


	void FixedUpdate () {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 50, Color.yellow);
        if (Physics.Raycast(ray, out hit, 50f))
        {
            HasTriggered isTriggered = hit.transform.GetComponent<HasTriggered>();
            if (!isTriggered)
            {
                Debug.Log("No HasTriggered script!");
                return;
            }

            isTriggered.Trigger();
            Debug.Log("Hit " + hit.point);
            point.transform.position = hit.point;
        }
        else
        {
            Debug.Log("No Hit!");
            point.transform.position = Vector3.zero;
        }

        
	}
}
