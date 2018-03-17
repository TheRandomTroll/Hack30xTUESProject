using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class TestRMS : MonoBehaviour
{
    int a, s, d, f;
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
    public static byte RNG(byte max)
    {
        byte[] rnd = new byte[1];
        rngCsp.GetBytes(rnd);
        return (byte)(rnd[0] % max);
    }
    private void Randomize()
    {
        a = RNG(3);
        s = RNG(3);
        d = RNG(3);
        f = RNG(3);
    }

    public RandomNew randomNew;

    public void Gen()
    {
        randomNew.GenMazeBase(20 + RNG(20), 20 + RNG(20));
        Randomize();
        randomNew.PlaceObstacle(5 + RNG(10), 5 + RNG(10), RNG(3), RNG(3), RNG(3), RNG(3));
        Randomize();
        randomNew.PlaceObstacle(30 + RNG(10), 5 + RNG(10), RNG(3), RNG(3), RNG(3), RNG(3));
        Randomize();
        randomNew.PlaceObstacle(30 + RNG(10), 30 + RNG(10), RNG(3), RNG(3), RNG(3), RNG(3));
        Randomize();
        randomNew.PlaceObstacle(5 + RNG(10), 30 + RNG(10), RNG(3), RNG(3), RNG(3), RNG(3));

    }
}
