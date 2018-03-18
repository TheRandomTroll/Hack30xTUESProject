using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

    public class RandomNew : MonoBehaviour
    {
        public Dictionary<Vector2Int, MapTypes.Spawn> randGrid = new Dictionary<Vector2Int, MapTypes.Spawn>();
        
        public void GenBlock(int x, int y, MapTypes.Spawn block)
        {
            randGrid.Add(new Vector2Int(x, y), MapTypes.Spawn.Cube);
        }

        public void SurroundObstacle(int x, int y, int up, int right, int down, int left)
        {
            for (int i = 0; i <= up; i++)
            {
                if (!randGrid.ContainsKey(new Vector2Int(x - 1, y + i)))
                     randGrid.Add(new Vector2Int(x - 1, y + i), MapTypes.Spawn.Point);
                if (!randGrid.ContainsKey(new Vector2Int(x + 1, y + i)))
                    randGrid.Add(new Vector2Int(x + 1, y + i), MapTypes.Spawn.Point);
            }
            for (int i = 0; i <= down; i++)
            {
                if (!randGrid.ContainsKey(new Vector2Int(x - 1, y - i)))
                    randGrid.Add(new Vector2Int(x - 1, y - i), MapTypes.Spawn.Point);
                if (!randGrid.ContainsKey(new Vector2Int(x + 1, y - i)))
                    randGrid.Add(new Vector2Int(x + 1, y - i), MapTypes.Spawn.Point);
            }
            for (int i = 0; i <= left; i++)
            {
               if (!randGrid.ContainsKey(new Vector2Int(x - i, y - i)))
                   randGrid.Add(new Vector2Int(x - i, y - 1), MapTypes.Spawn.Point);
               if (!randGrid.ContainsKey(new Vector2Int(x - i, y + 1)))
                  randGrid.Add(new Vector2Int(x - i, y + 1), MapTypes.Spawn.Point);
            }
            for (int i = 0; i <= right; i++)
            {
                if (!randGrid.ContainsKey(new Vector2Int(x + i, y - 1)))
                    randGrid.Add(new Vector2Int(x + i, y - 1), MapTypes.Spawn.Point);
                if (!randGrid.ContainsKey(new Vector2Int(x + i, y + 1)))
                    randGrid.Add(new Vector2Int(x + i, y + 1), MapTypes.Spawn.Point);
            }
            if (!randGrid.ContainsKey(new Vector2Int(x + right + 1, y-1)))
                randGrid.Add(new Vector2Int(x + right + 1, y-1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x - left - 1, y-1)))
                randGrid.Add(new Vector2Int(x - left - 1, y-1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x-1, y + up + 1)))
                randGrid.Add(new Vector2Int(x-1, y + up + 1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x-1, y - down - 1)))
                randGrid.Add(new Vector2Int(x-1, y - down - 1), MapTypes.Spawn.Point);

            if (!randGrid.ContainsKey(new Vector2Int(x + right + 1, y+1)))
                randGrid.Add(new Vector2Int(x + right + 1, y+1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x - left - 1, y+1)))
                randGrid.Add(new Vector2Int(x - left - 1, y+1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x+1, y + up + 1)))
                randGrid.Add(new Vector2Int(x+1, y + up + 1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x+1, y - down - 1)))
                randGrid.Add(new Vector2Int(x+1, y - down - 1), MapTypes.Spawn.Point);

            if (!randGrid.ContainsKey(new Vector2Int(x + right + 1, y)))
                randGrid.Add(new Vector2Int(x + right + 1, y), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x - left - 1, y)))
                randGrid.Add(new Vector2Int(x - left - 1, y), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x, y + up + 1)))
                randGrid.Add(new Vector2Int(x, y + up + 1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x, y - down - 1)))
                randGrid.Add(new Vector2Int(x, y - down - 1), MapTypes.Spawn.Point);
    }

        public int GetSpawnType(int x, int y) //to be fixed, 5 e i sam braindead
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
        public void GenObstacle(int x, int y, int up, int right, int down, int left)
        {
            if (!randGrid.ContainsKey(new Vector2Int(x, y)))
                randGrid.Add(new Vector2Int(x, y), MapTypes.Spawn.Cube);
        int i;
            for (i = 0; i < up; i++)
            {
                if(!randGrid.ContainsKey(new Vector2Int(x, y + i+1 )))
                    randGrid.Add(new Vector2Int(x, y + i +1 ), MapTypes.Spawn.Cube);
            }
            for (i = 0; i < down; i++)
            {
                if (!randGrid.ContainsKey(new Vector2Int(x, y -i -1)))
                    randGrid.Add(new Vector2Int(x, y - i-1), MapTypes.Spawn.Cube);
            }
            for (i = 0; i < left; i++)
            {
                if (!randGrid.ContainsKey(new Vector2Int(x - i -1, y)))
                    randGrid.Add(new Vector2Int(x - i -1 , y), MapTypes.Spawn.Cube);
            }
            for (i = 0; i < right; i++)
            {
                if (!randGrid.ContainsKey(new Vector2Int(x + i +1, y)))
                    randGrid.Add(new Vector2Int(x + i + 1, y), MapTypes.Spawn.Cube);
            }
        }

        public void GenMazebase(int x, int y)
        {
            for (int i = 0; i < 5; i++)
            {
                randGrid.Add(new Vector2Int(16 + i, y), MapTypes.Spawn.Point);
            }
            GenGhostSpawn(16, y - 1);
            randGrid.Add(new Vector2Int(16 + 2, y + 1), MapTypes.Spawn.Point);
            
        }
        public void GenGhostSpawn(int x, int y)
        {
            for (int i = 0; i < 3; i++)
                for (int k = 0; k < 5; k++)
                    randGrid.Add(new Vector2Int(x + k, y - i), MapTypes.Spawn.Cube);
            randGrid.Remove(new Vector2Int(x + 2, y));
            randGrid.Remove(new Vector2Int(x + 1, y-1));
            randGrid.Remove(new Vector2Int(x + 2, y-1));
            randGrid.Remove(new Vector2Int(x + 3, y-1));
            randGrid.Add(new Vector2Int(x + 2, y), MapTypes.Spawn.Ghost);
            randGrid.Add(new Vector2Int(x + 1, y - 1), MapTypes.Spawn.Ghost);
            randGrid.Add(new Vector2Int(x + 2, y - 1), MapTypes.Spawn.Ghost);
            randGrid.Add(new Vector2Int(x + 3, y - 1), MapTypes.Spawn.Ghost);
        }
        public void Mirror()
        {
        for (int i = 0; i < 18; i++)
            {
            for (int k = 0; k < 14; k++)
                {
                    randGrid.Add(new Vector2Int(35 - i, 14 - k), randGrid[new Vector2Int(i, k)]);
                }
            }
        }
}