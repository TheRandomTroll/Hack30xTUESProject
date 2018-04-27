using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : MonoBehaviour {

    [SerializeField] Inputs PS4Inputs;
    [SerializeField] Inputs MocuteInputs;
    [SerializeField] Inputs PCInputs;

    [SerializeField] private Inputs selectedInput;

    void Start ()
    {
        SetInputDevice();
    }

    private void SetInputDevice()
    {
        string[] connectedJoysticks = Input.GetJoystickNames();

        foreach (string joystick in Input.GetJoystickNames())
        {
            if (joystick == "Wireless Controller")
            {
                Debug.Log("Connected PS4 controller!");
                selectedInput = PS4Inputs;
                return;
            }
            else if (joystick == "MOCUTE-050_S10-XJMTK")
            {
                Debug.Log("Connected Ilya controller!");
                selectedInput = MocuteInputs;
                return;
            }
        }
        Debug.Log("Setting up keyboard!");
        selectedInput = PCInputs;
    }


    public bool GetLeft()
    {
        return Input.GetKey(selectedInput.left);
    }

    public bool GetLeftDown()
    {
        return Input.GetKeyDown(selectedInput.left);
    }


    public bool GetForward()
    {
        return Input.GetKey(selectedInput.top);
    }

    public bool GetForwardDown()
    {
        return Input.GetKeyDown(selectedInput.top);
    }


    public bool GetRight()
    {
        return Input.GetKey(selectedInput.right);
    }

    public bool GetRightDown()
    {
        return Input.GetKeyDown(selectedInput.right);
    }

    public bool GetBackward()
    {
        return Input.GetKey(selectedInput.down);
    }

    public bool GetBackwardDown()
    {
        return Input.GetKeyDown(selectedInput.down);
    }


    public bool GetLeftOne()
    {
        return Input.GetKey(selectedInput.leftOne);
    }

    public bool GetLeftOneDown()
    {
        return Input.GetKeyDown(selectedInput.leftOne);
    }


    public bool GetLeftTwo()
    {
        return Input.GetKey(selectedInput.leftTwo);
    }


    public bool GetLeftTwoDown()
    {
        return Input.GetKeyDown(selectedInput.leftTwo);
    }


    public bool GetRightOne()
    {
        return Input.GetKey(selectedInput.rightOne);
    }


    public bool GetRightOneDown()
    {
        return Input.GetKeyDown(selectedInput.rightOne);
    }


    public bool GetRightTwo()
    {
        return Input.GetKey(selectedInput.rightTwo);
    }


    public bool GetRightTwoDown()
    {
        return Input.GetKeyDown(selectedInput.rightTwo);
    }


    public bool GetBack()
    {
        return Input.GetKey(selectedInput.backButton);
    }


    public bool GetBackDown()
    {
        return Input.GetKeyDown(selectedInput.backButton);
    }
}
