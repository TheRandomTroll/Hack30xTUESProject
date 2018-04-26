using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAnimation : MonoBehaviour {

    [SerializeField] GameObject pacmanClosed;
    [SerializeField] GameObject pacmanOpen;


    private void Start()
    {
        StartCoroutine(Animation());
    }


    private IEnumerator Animation()
    {
        while (true)
        {
            pacmanClosed.GetComponent<MeshRenderer>().enabled = !pacmanClosed.GetComponent<MeshRenderer>().enabled;
            pacmanOpen.GetComponent<MeshRenderer>().enabled = !pacmanOpen.GetComponent<MeshRenderer>().enabled;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
