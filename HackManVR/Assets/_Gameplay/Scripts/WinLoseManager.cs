using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour {

    public virtual void WinExecution()
    {
        throw new NotImplementedException("Implement in a child!");
    }

    public virtual void LoseExecution()
    {
        throw new NotImplementedException("Implement in a child!");
    }
}
