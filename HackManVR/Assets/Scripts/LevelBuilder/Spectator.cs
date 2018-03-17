﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    public int speed = 20;

    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        if (Input.GetButton("Backward"))
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }

        if(Input.GetButton("Forward"))
        {

            transform.position += -transform.up * speed * Time.deltaTime;
        }

        /*
        if (Input.GetKey(KeyCode.W))
            this.transform.position += this.transform.forward * this.speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            this.transform.position += this.transform.forward * -1 * this.speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            this.transform.position += this.transform.right * this.speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            this.transform.position += this.transform.right * -1 * this.speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
            this.transform.position += this.transform.up * this.speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
            this.transform.position += this.transform.up * -1 * this.speed * Time.deltaTime;
        */
    }
}