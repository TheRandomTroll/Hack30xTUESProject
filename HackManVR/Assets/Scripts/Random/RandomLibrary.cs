using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class RandomLibrary : MonoBehaviour
{

    public int MAX = 50;
    enum Spawn {Cube, Pac, Ghost, Cherry, Portal, Point};
    int? [,] mapArray = new int?[50,50];
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

    

    public static byte RNG(byte max)
    {
        byte[] rnd = new byte[1];
        rngCsp.GetBytes(rnd);
        return (byte)(rnd[0] % max);
    }
    public void GenPointsOnFree()
    {
        for (int i = 0; i < 49; i++)
        {           
            for (int k = 0; k < 49; k++)
            {
                if (mapArray[i, k] == null)
                    mapArray[i, k] = (int)Spawn.Point;
            }
        }
    }
    public void GenAllPoints()
    {
        for(int i = 0; i < 49; i++)
        {
            for(int k = 0; k < 49; k++)
            {
                mapArray[i, k] = (int)Spawn.Point;
            }
        }
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
        mapArray[Xp, 0] = (int)Spawn.Portal;
        mapArray[Xp, 49] = (int)Spawn.Portal;
        mapArray[0, Yp] = (int)Spawn.Portal;
        mapArray[49, Yp] = (int)Spawn.Portal;  //clearvam areata pred portalite
        mapArray[Xp, 1] = null;
        mapArray[Xp, 48] = null;
        mapArray[1, Yp] = null;
        mapArray[48, Yp] = null;
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
