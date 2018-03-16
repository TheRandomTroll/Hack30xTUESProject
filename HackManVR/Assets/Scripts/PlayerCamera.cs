using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    // Otkazal sum se ot tui. ~Krasi

    public GameObject player;
    private Vector3 offset;

    float distance;
    Vector3 playerPrevPos, playerMoveDir;
    Vector3 lastRotation;

    private bool isRotating = false;

    void Start()
    {
        offset = transform.position - player.transform.position;
        distance = offset.magnitude;
        playerPrevPos = player.transform.position;
        lastRotation = player.transform.rotation.eulerAngles;
    }

    void LateUpdate()
    {
        playerMoveDir = player.transform.position - playerPrevPos;
        if (playerMoveDir != Vector3.zero)
        {
            playerMoveDir.Normalize();
            Vector3 newPos = player.transform.position - playerMoveDir * distance;
            newPos.y += 3;
            transform.position = newPos;
            Vector3 currentRotation = player.transform.rotation.eulerAngles;
            if (lastRotation != currentRotation)
            {
                StopAllCoroutines();
                StartCoroutine(RotateTo(
                    Quaternion.Euler(lastRotation),
                    Quaternion.Euler(currentRotation), 
                    0.5f));
            }
            playerPrevPos = player.transform.position;
            lastRotation = currentRotation;
        }
    }

    public IEnumerator RotateTo(Quaternion start, Quaternion end, float time)
    {
        float currentTime = 0;
        float lerpCoefficient = 0;
        while(lerpCoefficient < 1)
        {
            currentTime += Time.deltaTime;
            lerpCoefficient = time / currentTime;
            Quaternion.Lerp(start, end, lerpCoefficient);
            yield return new WaitForEndOfFrame();
        }
    }
}
