using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNumber : MonoBehaviour {

    [SerializeField]
    HasTriggered left;
    [SerializeField]
    HasTriggered right;
    [SerializeField]
    TextMesh indexText;

    [SerializeField]
    private int maxIndex = 10;
    private int index = 0;

	void Update () {
        indexText.text = index + "";

        if(left.GetTrigger() && (Input.GetButtonDown("Left") || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if (index > 0)
            {
                index--;
            }
        }

        if (right.GetTrigger() && (Input.GetButtonDown("Left") || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if (index < maxIndex)
            {
                index++;
            }
        }
    }

    public int GetIndex()
    {
        return index;
    }
}
