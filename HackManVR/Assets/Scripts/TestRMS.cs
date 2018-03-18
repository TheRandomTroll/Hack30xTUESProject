using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class TestRMS : MonoBehaviour
{
    public RandomNew randomNew;
    public void Start()
    { 
        
        randomNew = GetComponent<RandomNew>();
        Gen();
    }

        private int a, s, d, f;

        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        public static byte RNG(byte max)
        {
            byte[] rnd = new byte[1];
            rngCsp.GetBytes(rnd);
            return (byte)(rnd[0] % max);
        }
        private void Randomize()
        {
            a = RNG(4);
            s = RNG(4);
            d = RNG(4);
            f = RNG(4);
        }

       
        public void Gen()
        {
            randomNew.GenMazeBase(20, 20);
            Randomize();
            randomNew.GenObstacle(19, 21, a, s, d, f);
            randomNew.SurroundObstacle(19, 21, a, s, d, f);
        }

    

}