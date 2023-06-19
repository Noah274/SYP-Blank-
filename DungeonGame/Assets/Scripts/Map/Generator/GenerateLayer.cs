using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using UnityEditor;

public class GenerateLayer
{
    public static int[,] mapArray;
    const int startX = 5;
    const int startY = 5;
    public System.Random _random = new System.Random();
    int[] quantityWay = { 0, 0, 0, 0};

    private GeneratorOptions gOptions;
    public GenerateLayer(GeneratorOptions gOptions)
    {
        this.gOptions = gOptions;
        //Debug.Log("GenerateLayer");
    }

    public int[,] makeRooms()
        {
            mapArray = new int[gOptions.layerArrayLength, gOptions.layerArrayLength];

            int quantityRooms = QuantityRoom();
            //Debug.Log(quantityRooms);
            selRoom(quantityRooms);
            Shuffle(quantityWay);

            GenerateRooms();
            spawnBossRoom();
            
            //printArray(mapArray, quantityRooms);

            return mapArray;
        }

        public void spawnBossRoom()
        {
            int[] posFathest = FindFarthestField(mapArray, startX, startY);
            mapArray[posFathest[0], posFathest[1]] = gOptions.bossRoom;
        }
        
        public void printArray(int[,] array, int quantityRooms)
        {
            Debug.Log(quantityRooms);
            Debug.Log("--"+quantityWay[0] + quantityWay[1] + quantityWay[2] + quantityWay[3]);
            
            for (int x = 0; x < gOptions.layerArrayLength; x++)
            {
                string msg = " ";
                for (int y = 0; y < gOptions.layerArrayLength; y++)
                {
                    msg += " - " + array[x, y];
                }

                Debug.Log(x +": "+msg);
            }

        }
        
        public void GenerateRooms(){
            mapArray[startX, startY] = gOptions.spawnRoom;
            
            if (quantityWay[0] != 0)
            {
                mapArray[startX+1, startY] = gOptions.branch1Value;
            }
            if (quantityWay[1] != 0)
            {
                mapArray[startX-1, startY] = gOptions.branch2Value;
            }
            if (quantityWay[2] != 0)
            {
                mapArray[startX, startY+1] = gOptions.branch3Value;
                
            }
            if (quantityWay[3] != 0)
            {
                mapArray[startX, startY-1] = gOptions.branch4Value;
            }
            
            GenerateBranch(mapArray, startX + 2, startY, gOptions.branch1Value, quantityWay[0]-1);
            GenerateBranch(mapArray, startX - 2, startY, gOptions.branch2Value, quantityWay[1]-1);
            GenerateBranch(mapArray, startX, startY + 2, gOptions.branch3Value, quantityWay[2]-1);
            GenerateBranch(mapArray, startX, startY - 2, gOptions.branch4Value, quantityWay[3]-1);
        }

        public void GenerateBranch(int[,] array, int x, int y, int value, int remainingBranches)
        {
            if (IsPositionInArray(mapArray, x, y) == false || remainingBranches <= 0)
            {
                return;
            }

            if (array[x, y] != 0)
            {
                return;
            }
            
            array[x, y] = value;
            
            if (remainingBranches > 0)
            {
                bool done = false;
                int count = 0;
                while (done == false)
                {
                    int direction = _random.Next(0, 4);
                    if (direction == 0)
                    {
                        if (IsPositionInArray(mapArray, x+1, y) && array[x+1, y] == 0)
                        {
                            done = true;
                            GenerateBranch(array, x + 1, y, value, remainingBranches - 1);
                        }

                    }
                    else if (direction == 1)
                    {
                        if (IsPositionInArray(mapArray, x-1, y) && array[x-1, y] == 0)
                        {
                            done = true;
                            GenerateBranch(array, x - 1, y, value, remainingBranches - 1);
                        }
                    }
                    else if (direction == 2)
                    {
                        if (IsPositionInArray(mapArray, x, y+1) && array[x, y+1] == 0)
                        {
                            done = true;
                            GenerateBranch(array, x, y + 1, value, remainingBranches - 1);
                        }
                    }
                    else
                    {
                        if (IsPositionInArray(mapArray, x, y-1) && array[x, y-1] == 0)
                        {
                            done = true;
                            GenerateBranch(array, x, y - 1, value, remainingBranches - 1);
                        }
                    }

                    if (count == 20)
                    {
                        done = true;
                    }

                    count++;
                }
            }
        }
        
        public bool IsPositionInArray(int[,] array, int x, int y)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);
            
            if (x >= 0 && x < rows && y >= 0 && y < columns)
            {
                return true;
            }

            return false;
        }
        
        public int QuantityRoom()
        {
            return(_random.Next(0, 4) + (5 + (gOptions.layerLevel * 2)));
        }
        
        public void selRoom(int quantityRooms)
        {
            int randomRange = _random.Next(1, 101); //zwischen 1 - 100
            int randomQuantityWay = _random.Next(1, quantityRooms-2);
            int num = 0;
            // 4 Räume = 65%
            // 3 Raum = 25%
            // 2 Räume = 10%
            // 1 Raum = 0%
            if (randomRange <= 10)
            {
                num = 2;
            }
            else if(randomRange <= 35)
            {
                num = 3;
            }
            else
            {
                num = 4;
            }
            // 4 Räume
            //  -Way1 = randomQuantityWay / 2
            //  -Way2 = randomQuantityWay / 2
            //  -Way3 = (quantityRooms - randomQuantityWay)/2
            //  -Way4 = (quantityRooms - randomQuantityWay)/2
            // 3 Raum
            //  -Way1 = randomQuantityWay / 2
            //  -Way2 = randomQuantityWay / 2
            //  -Way3 = quantityRooms - randomQuantityWay
            // 2 Räume
            //  -Way1 = randomQuantityWay
            //  -Way2 = quantityRooms - randomQuantityWay
            // 1 Raum
            //  -Way1 = quantityRooms
            
            //Überarbeiten 
            if (num == 2)
            {
                quantityWay[0] = randomQuantityWay;
                quantityWay[1] = quantityRooms - randomQuantityWay;
            }
            else if (num == 3)
            {
                quantityWay[0] = randomQuantityWay / 2;
                quantityWay[1] = randomQuantityWay / 2;
                quantityWay[2] = quantityRooms - randomQuantityWay;
            }
            else if (num == 4)
            {
                quantityWay[0] = randomQuantityWay / 2;
                quantityWay[1] = randomQuantityWay / 2;
                quantityWay[2] = (quantityRooms - randomQuantityWay) /2;
                quantityWay[3] = (quantityRooms - randomQuantityWay) /2;
            }
        }

        public void Shuffle(int[] array)
        {
            int[] rooms = new int[4];

            for (int i = 0; i < 4; i++)
            {
                int next = _random.Next(0,4);
                while (true)
                {
                    if (rooms[next] == 0) 
                    {
                        rooms[next] = array[i];
                        break;
                    }
                    else
                    {
                        next= _random.Next(0,4);
                    }
                }
            }

            quantityWay = rooms;
        }
        
    public int[] FindFarthestField(int[,] array,  int centerX,  int centerY)
    { 
        double farthestDistance = 0;
        int[] farthestPosition = new int[2];
        for (int i = 0; i < array.GetLength(0); i++)
        { 
            for (int j = 0; j < array.GetLength(1); j++)
            { 
                if (array[i,j] != 0)
                { 
                    double distance = Math.Sqrt(Math.Pow(centerX - i, 2) + Math.Pow(centerY - j, 2)); 
                    if (distance > farthestDistance) 
                    { 
                        farthestDistance = distance; 
                        farthestPosition[0] = i; 
                        farthestPosition[1] = j;
                    } 
                }
            }
        }

        return farthestPosition;
    }
}

