using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    [SerializeField] private int points = 100;
    [SerializeField] private AudioClip source;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PointManager>().AddPoints(points);
            Destroy(gameObject);
            // Todo: ParticleSystem oncollect
            if(source)
                AudioSource.PlayClipAtPoint(source, transform.position);
        }
    }
}
