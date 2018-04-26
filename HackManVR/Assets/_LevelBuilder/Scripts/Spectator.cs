using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    public int speed = 20;

    void Update()
    {
        transform.position += camera.transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += camera.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        if (Input.GetButton("Forward"))
        {
            transform.position += camera.transform.up * speed * Time.deltaTime;
        }

        if(Input.GetButton("Backward"))
        {
            transform.position += -camera.transform.up * speed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.W))
            transform.position += camera.transform.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position += camera.transform.forward * -1 * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += camera.transform.right * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.position += camera.transform.right * -1 * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
            transform.position += camera.transform.up * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
            transform.position += camera.transform.up * -1 * speed * Time.deltaTime;
    }
}