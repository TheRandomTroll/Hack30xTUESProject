using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour {

    private void Start()
    {
        string[] joysticks = Input.GetJoystickNames();
        foreach(string joystick in joysticks)
        {
            Debug.Log("Connected" + joystick);
        }
    }

    void Update() {
        System.Array values = System.Enum.GetValues(typeof(KeyCode));
        foreach (KeyCode code in values)
        {
            if (Input.GetKeyDown(code)) { print(System.Enum.GetName(typeof(KeyCode), code)); }
        }

        /*
        foreach (string joystick in Input.GetJoystickNames())
        {
            if (joystick == "Wireless Controller")
            {
                Debug.Log("Connected PS4 controller!");
            }
            else if (joystick == "MOCUTE-050_S10-XJMTK")
            {
                Debug.Log("Connected Ilya controller!");
            }
        }
        */

    }
}
