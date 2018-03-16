using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public double mouseX, mouseY;

    [Range(0.05f, 2)]
    public double sensitivity;

    [Range(0, 100)]
    public double smoothness;

    public bool inputActive = true;
    public bool mouseLocked = true;
    public bool invertedY = false;
    private Quaternion eulerRotation;

    void Update()
    {
        Cursor.lockState = mouseLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !mouseLocked;

        if (inputActive)
        {
            //Cursor.lockState = mouseLocked ? CursorLockMode.Locked : CursorLockMode.Confined;
            //Cursor.visible = !mouseLocked;

            mouseX += Input.GetAxis("Mouse X") * sensitivity;

            double differenceY = Input.GetAxis("Mouse Y") * sensitivity;
            mouseY += invertedY ? differenceY : -differenceY;

            mouseY = Mathf.Clamp((float)mouseY, -90, 90);

            eulerRotation = Quaternion.Euler((float)mouseY, (float)mouseX, 0.0f);

            if (sensitivity == 0.0)
            {
                transform.rotation = eulerRotation;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, eulerRotation, (float)smoothness * Time.deltaTime);
            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            mouseLocked = mouseLocked ? false : true;
            inputActive = inputActive ? false : true;
        }
    }
}