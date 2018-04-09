using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigpoint : Point {
    
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<MovementPacMan>().EatBigpoint();
        }
        base.OnTriggerEnter(other);
    }
}
