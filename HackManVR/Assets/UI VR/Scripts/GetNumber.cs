using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNumber : MonoBehaviour {

    [SerializeField]
    UITrigger left;
    [SerializeField]
    UITrigger right;
    [SerializeField]
    TextMesh indexText;

    [SerializeField]
    private int maxIndex = 10;
    private int index = 0;

	void Update () {
        if(left.GetTrigger() && index > 0)
        {
            index--;
        }

        if (right.GetTrigger() && index < maxIndex)
        {
            index++;
        }
        indexText.text = index + "";
    }

    public int GetIndex()
    {
        return index;
    }
}
