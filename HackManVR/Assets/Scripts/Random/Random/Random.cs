using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class RNGCSP
{
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
    public static byte RNG(byte max)
    {
        byte[] rnd = new byte[1];
        rngCsp.GetBytes(rnd);
        return (byte)(rnd[0] % max);
    }
}

public class Random : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
