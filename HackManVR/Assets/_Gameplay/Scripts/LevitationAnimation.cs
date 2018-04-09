using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationAnimation : MonoBehaviour {

    [SerializeField] private float period = 2;
    [SerializeField] private float levitationHeight = 1;
    private const float tau = 2 * Mathf.PI;
    private Vector3 startPosition;
    private Vector3 endPosition;
	void Start () {
        startPosition = transform.position;
        endPosition = startPosition;
        endPosition.y += levitationHeight;
	}
	

	void Update () {
        
        float cycles = Time.time / period;
        float rawSineWave = Mathf.Sin(cycles * tau);

        float movementFactor = rawSineWave / 2f + 0.5f;

        transform.position = Vector3.Lerp(
            startPosition,
            endPosition,
            movementFactor
        );
    }
}
