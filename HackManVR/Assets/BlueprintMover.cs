using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintMover : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            MovePlacedObjects(Vector3.left);

        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            MovePlacedObjects(Vector3.right);

        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
            MovePlacedObjects(Vector3.back);

        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
            MovePlacedObjects(Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Comma))
            MovePlacedObjects(Vector3.down);

        if (Input.GetKeyDown(KeyCode.Period))
            MovePlacedObjects(Vector3.up);
    }

    private void MovePlacedObjects(Vector3 direction)
    {
        bool moveable = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!(transform.GetChild(i).position + direction).IsWithinBounds())
            {
                moveable = false;
            }
        }

        if (moveable)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).position += direction;
            }
        }
    }
}