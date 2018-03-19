using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVRRay : MonoBehaviour {

    [SerializeField]
    GameObject point;


    private void Start()
    {
        point = Instantiate(point);
        point.GetComponent<MeshRenderer>().enabled = false;
    }


    void FixedUpdate () {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 50, Color.yellow);

        if (Physics.Raycast(ray, out hit, 50f))
        {
            UITrigger isTriggered = hit.transform.GetComponent<UITrigger>();
            if (!isTriggered) return;
            

            isTriggered.Trigger();
            point.GetComponent<MeshRenderer>().enabled = true;
            point.transform.position = hit.point;
        }
        else
        {
            point.GetComponent<MeshRenderer>().enabled = false;
            point.transform.position = Vector3.zero;
        }
	}
}
