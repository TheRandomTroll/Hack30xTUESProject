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
            /*for (int i = 0; i <= up; i++)
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
            }*/
            /*for (int i = 0; i <= left; i++)
            {
               if (!randGrid.ContainsKey(new Vector2Int(x - i, y - i)))
                   randGrid.Add(new Vector2Int(x - i, y - 1), MapTypes.Spawn.Point);
               if (!randGrid.ContainsKey(new Vector2Int(x - i, y + 1)))
                  randGrid.Add(new Vector2Int(x - i, y + 1), MapTypes.Spawn.Point);
            }*/
            for (int i = 0; i <= right; i++)
            {
                if (!randGrid.ContainsKey(new Vector2Int(x + i, y - 1)))
                    randGrid.Add(new Vector2Int(x + i, y - 1), MapTypes.Spawn.Point);
                if (!randGrid.ContainsKey(new Vector2Int(x + i, y + 1)))
                    randGrid.Add(new Vector2Int(x + i, y + 1), MapTypes.Spawn.Point);
            }
            /*if (!randGrid.ContainsKey(new Vector2Int(x + right + 1, y-1)))
                randGrid.Add(new Vector2Int(x + right + 1, y-1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x - left - 1, y-1)))
                randGrid.Add(new Vector2Int(x - left - 1, y-1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x-1, y + up + 1)))
                randGrid.Add(new Vector2Int(x-1, y + up + 1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x-1, y - down - 1)))
                randGrid.Add(new Vector2Int(x-1, y - down - 1), MapTypes.Spawn.Point);*/

            /*if (!randGrid.ContainsKey(new Vector2Int(x + right + 1, y+1)))
                randGrid.Add(new Vector2Int(x + right + 1, y+1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x - left - 1, y+1)))
                randGrid.Add(new Vector2Int(x - left - 1, y+1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x+1, y + up + 1)))
                randGrid.Add(new Vector2Int(x+1, y + up + 1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x + 1, y - down - 1)))
                randGrid.Add(new Vector2Int(x + 1, y - down - 1), MapTypes.Spawn.Point);*/

            /*if (!randGrid.ContainsKey(new Vector2Int(x + right + 1, y)))
                randGrid.Add(new Vector2Int(x + right + 1, y), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x - left - 1, y)))
                randGrid.Add(new Vector2Int(x - left - 1, y), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x, y + up + 1)))
                randGrid.Add(new Vector2Int(x, y + up + 1), MapTypes.Spawn.Point);
            if (!randGrid.ContainsKey(new Vector2Int(x, y - down - 1)))
                randGrid.Add(new Vector2Int(x, y - down - 1), MapTypes.Spawn.Point);*/
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

        public void GenMazeBase(int x, int y)
        {
            for (int i = 0; i < 5; i++)
            {
                randGrid.Add(new Vector2Int(x-4 + i, y), MapTypes.Spawn.Point);
            }
            GenGhostSpawn(x-4, y - 1);
            randGrid.Add(new Vector2Int(x-4+2, y + 1), MapTypes.Spawn.Point);
            
        }
        public void GenGhostSpawn(int x, int y)
        {
           for (int i = 0; i < 3; i++)
                for (int k = 0; k < 5; k++)
                    randGrid.Add(new Vector2Int(x + k, y - i), MapTypes.Spawn.Cube);
            for (int i = 0; i < 7; i++)
                randGrid.Add(new Vector2Int(x + i - 1, y - 3), MapTypes.Spawn.Point);
            for (int i = 0; i < 4; i++)
                randGrid.Add(new Vector2Int(x + 5, y +i-2), MapTypes.Spawn.Point);
            for (int i = 0; i < 4; i++)
                randGrid.Add(new Vector2Int(x -1, y + i-2), MapTypes.Spawn.Point);
            randGrid.Remove(new Vector2Int(x + 2, y));
            randGrid.Remove(new Vector2Int(x + 1, y - 1));
            randGrid.Remove(new Vector2Int(x + 2, y - 1));
            randGrid.Remove(new Vector2Int(x + 3, y - 1));
            randGrid.Add(new Vector2Int(x + 2, y), MapTypes.Spawn.Ghost);
            randGrid.Add(new Vector2Int(x + 1, y - 1), MapTypes.Spawn.Ghost);
            randGrid.Add(new Vector2Int(x + 2, y - 1), MapTypes.Spawn.Ghost);
            randGrid.Add(new Vector2Int(x + 3, y - 1), MapTypes.Spawn.Ghost);
            randGrid.Remove(new Vector2Int(x + 2, y - 3));
            randGrid.Add(new Vector2Int(x + 2, y - 3), MapTypes.Spawn.Pac);
        }
        public void Mirror()
        {
        for (int i = 0; i < 15; i++)
            {
            for (int k = 0; k < 30; k++)
                {
                    randGrid.Add(new Vector2Int(31 - i, k), randGrid[new Vector2Int(i, k)]);
                }
            }
        }
}