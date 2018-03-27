using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour {

    UITrigger trigger;

    [SerializeField]
    GetNumber getNumber;

    void Start () {
        trigger = GetComponent<UITrigger>();
	}
	

	void Update () {
        if (trigger.GetTrigger()) {
            foreach (CanRemove obj in FindObjectsOfType<CanRemove>())
            {
                Destroy(obj.gameObject);
                // TODO: Instantiate grid in place of destroyed objects!.
            }
            FindObjectOfType<LoadLevel>().Load(getNumber.GetIndex());
            
        }
    }
}
