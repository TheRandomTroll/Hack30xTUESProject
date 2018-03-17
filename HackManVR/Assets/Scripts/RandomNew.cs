using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class RandomNew : MonoBehaviour
{
    public Dictionary<Vector2Int, MapTypes.Spawn> randGrid = new Dictionary<Vector2Int, MapTypes.Spawn>();
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

    public int XM;   
    public int YM;



    public static byte RNG(byte max)
    {
        byte[] rnd = new byte[1];
        rngCsp.GetBytes(rnd);
        return (byte)(rnd[0] % max);
    }

    private void GenBlock(int x, int y,  MapTypes.Spawn block)
    {
        randGrid.Add(new Vector2Int(x, y), MapTypes.Spawn.Cube);
    }

    private void SurroundObstacle(int x, int y, int up, int right, int down, int left)      
    {
        for (int i = 0; i < up; i++)
        {
            randGrid.Add(new Vector2Int(x-1, y + i), MapTypes.Spawn.Point);
            randGrid.Add(new Vector2Int(x+1, y + i), MapTypes.Spawn.Point);
        }
        for (int i = 0; i < down; i++)
        {
            randGrid.Add(new Vector2Int(x-1, y - i), MapTypes.Spawn.Point);
            randGrid.Add(new Vector2Int(x+1, y - i), MapTypes.Spawn.Point);
        }
        for (int i = 0; i < left; i++)
        {
            randGrid.Add(new Vector2Int(x - i, y-1), MapTypes.Spawn.Point);
            randGrid.Add(new Vector2Int(x - i, y+1), MapTypes.Spawn.Point);
        }
        for (int i = 0; i < right; i++)
        {
            randGrid.Add(new Vector2Int(x + i, y-1), MapTypes.Spawn.Point);
            randGrid.Add(new Vector2Int(x + i, y+1), MapTypes.Spawn.Point);
        }
        randGrid.Add(new Vector2Int(x + right + 1, y), MapTypes.Spawn.Point);
        randGrid.Add(new Vector2Int(x - left - 1, y), MapTypes.Spawn.Point);
        randGrid.Add(new Vector2Int(x, y + up + 1), MapTypes.Spawn.Point); 
        randGrid.Add(new Vector2Int(x, y - down - 1), MapTypes.Spawn.Point);
    }

    private int GetSpawnType(int x, int y) //to be fixed, 5 e i sam braindead
    {
        if (randGrid[new Vector2Int(x + 1, y)] == MapTypes.Spawn.Point && randGrid[new Vector2Int(x, y + 1)] == MapTypes.Spawn.Point)
            return 1;
        if (randGrid[new Vector2Int(x + 1, y)] == MapTypes.Spawn.Point && randGrid[new Vector2Int(x, y - 1)] == MapTypes.Spawn.Point)
            return 2;
        if (randGrid[new Vector2Int(x - 1, y)] == MapTypes.Spawn.Point && randGrid[new Vector2Int(x, y - 1)] == MapTypes.Spawn.Point)
            return 3;
        if (randGrid[new Vector2Int(x - 1, y)] == MapTypes.Spawn.Point && randGrid[new Vector2Int(x, y + 1)] == MapTypes.Spawn.Point)
            return 4;
        return 0;


    }
    private void PlaceObstacle(int x, int y, int up, int right, int down, int left)
    {
        for(int i = 0; i < up; i++)
        {
            
                randGrid.Add(new Vector2Int(x, y + i), MapTypes.Spawn.Cube);
        }
        for (int i = 0; i < down; i++)
        {
            
                randGrid.Add(new Vector2Int(x, y - i), MapTypes.Spawn.Cube);
        }
        for (int i = 0; i < left; i++)
        {
            
                randGrid.Add(new Vector2Int(x - i, y), MapTypes.Spawn.Cube);
        }
        for (int i = 0; i < right; i++)
        {
            
                randGrid.Add(new Vector2Int(x + i, y), MapTypes.Spawn.Cube);
        } 
    }

    private void GenMazeBase(int x, int y, int rand, ref int XM, ref int YM)
    {
        for(int i = 0; i < 5; i++)
        {
            randGrid.Add(new Vector2Int(x+i, y), MapTypes.Spawn.Point);
        }
        GenGhostSpawn(x, y + 1);
        if (rand == 2)                  //hardcoded shema za po dob1r spawn
            rand = 3; 
        randGrid.Add(new Vector2Int(x + rand, y-1), MapTypes.Spawn.Point);
        XM = x + rand;
        YM = y - 1;
    }
    private void GenGhostSpawn(int x, int y)
    {
        for(int i = 0; i < 3; i++)
            for (int k = 0; k < 5; k++)
                randGrid.Add(new Vector2Int(x+k, y+i), MapTypes.Spawn.Cube);
        randGrid.Add(new Vector2Int(x+2, y), MapTypes.Spawn.Ghost);
        randGrid.Add(new Vector2Int(x+1, y+1), MapTypes.Spawn.Ghost);
        randGrid.Add(new Vector2Int(x+2, y+1), MapTypes.Spawn.Ghost);
        randGrid.Add(new Vector2Int(x+3, y+1), MapTypes.Spawn.Ghost);
    }
}
