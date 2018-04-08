using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotation : MonoBehaviour {

    // README: Nqmam nikakva ideq kak tova raboti.

    [SerializeField] private float period = 1f;

    [SerializeField] private float movementFactor; // TODO: Remove SerializeField


	void Start () {
        StartCoroutine(Rotate());
	}
	
    private IEnumerator Rotate()
    {
        const float tau = Mathf.PI * 2;


        while (true)
        {
            float cycles = Time.time / period;
            float rawSineWave = Mathf.Sin(cycles * tau);

            movementFactor = rawSineWave / 2f + 0.5f;

            transform.Rotate(new Vector3(movementFactor, movementFactor, movementFactor));
            yield return new WaitForEndOfFrame();
        }

    }
}
