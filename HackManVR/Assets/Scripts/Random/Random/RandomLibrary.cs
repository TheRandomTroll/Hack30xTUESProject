using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class RandomLibrary : MonoBehaviour {

    public int MAX = 50;
    private int x;
    private int y;
    enum Spawn {Cube, Pac, Ghost, Cherry, Portal, Null};
    int [,] mapArray = new int[50,50];
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

    public static byte RNG(byte max)
    {
        byte[] rnd = new byte[1];
        rngCsp.GetBytes(rnd);
        return (byte)(rnd[0] % max);
    }

    public void GenOuterWall()
    {
        for (int i = 0; i < 49; i++)
        {
            mapArray[i, 0] = (int)Spawn.Cube;
            mapArray[0, 0-i] = (int)Spawn.Cube;    
            mapArray[i, 49] = (int)Spawn.Cube;
            mapArray[49, i] = (int)Spawn.Cube;
        }
    }

    public void GenPortals()
    {
        int Xp = RNG(50);
        int Yp = RNG(50);
        mapArray[x, 0] = (int)Spawn.Portal;
        mapArray[x, 49] = (int)Spawn.Portal;
        mapArray[0, y] = (int)Spawn.Portal;
        mapArray[49, y] = (int)Spawn.Portal;  //clearvam areata pred portalite
        mapArray[x, 1] = (int)Spawn.Null;
        mapArray[x, 48] = (int)Spawn.Null;
        mapArray[1, y] = (int)Spawn.Null;
        mapArray[48, y] = (int)Spawn.Null;
    }
    public void GenCross(int x, int y)
    {
        for(int i = 0; i < RNG(4); i++)
        {
            mapArray[x, y + i] = (int)Spawn.Cube;
        }
        for (int i = 0; i < RNG(4); i++)
        {
            mapArray[x + i, y] = (int)Spawn.Cube;
        }
        for (int i = 0; i < RNG(4); i++)
        {
            mapArray[x, y - i] = (int)Spawn.Cube;
        }
        for (int i = 0; i < RNG(4); i++)
        {
            mapArray[x - i, y] = (int)Spawn.Cube;
        }
    }

    public void GenGhostArea(int x, int y)
    {
        for(int i = 0; i < 5; i++)
        {
            mapArray[x + i , y] = (int)Spawn.Cube;
            if (i == 2)
                mapArray[x + i, y] = (int)Spawn.Ghost;
        }
        mapArray[x , y - 1] = (int)Spawn.Cube;
        for (int i = 0; i < 3; i++)
            mapArray[x + 1 + i , y - 1] = (int)Spawn.Ghost;
        mapArray[x + 4 , y - 1] = (int)Spawn.Cube;
        for (int i = 0; i < 5; i++)
            mapArray[x+i , y - 2] = (int)Spawn.Cube;
    }

    public void GenCube(int x, int y)
    {
        mapArray[x, y] = (int)Spawn.Cube;
    }

    public void GenRodX(int x, int y)
    {
        for (int i = 0; i < RNG(3); i++)
            mapArray[x + i, y] = (int)Spawn.Cube;
        for(int i = 0; i < RNG(3); i++)
            mapArray[x - i, y] = (int)Spawn.Cube;
    }

    public void GenRodY(int x, int y)
    {
        for (int i = 0; i < RNG(3); i++)
            mapArray[x, y - i] = (int)Spawn.Cube;
        for (int i = 0; i < RNG(3); i++)
            mapArray[x, y + i] = (int)Spawn.Cube;
    }
}
