using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRotation : MonoBehaviour {

    // README: Nqmam nikakva ideq kak tova raboti.

    [SerializeField] private float period = 1f;
    [SerializeField] private float rotationsPerPeriod = 2.5f;
    [SerializeField] private float movementFactor; // TODO: Remove SerializeField



    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 endRotation;

    void Start () {
        Vector3 rotation = transform.rotation.eulerAngles;
        startRotation = rotation;
        endRotation = new Vector3(
            360 * rotationsPerPeriod + rotation.x,
            360 * rotationsPerPeriod + rotation.y,
            360 * rotationsPerPeriod + rotation.z);
	}


    private void Update()
    {
        const float tau = Mathf.PI * 2;
        float cycles = Time.time / period;
        float rawSineWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSineWave / 2f + 0.5f;

        transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, movementFactor);
    }
}
