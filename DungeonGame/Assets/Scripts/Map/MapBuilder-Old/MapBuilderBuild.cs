using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Tilemaps;
using System.IO;
using Newtonsoft.Json;


public class MapBuilderBuild
{
    
    private int wigth;
    private int higth;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public string[,] buildMap =
    {
        { "null", "null", "null", "doorUp", "null", "null", "null" },
        { "null", "null", "null", "null", "null", "null", "null" },
        { "null", "null", "null", "null", "null", "null", "null" },
        { "doorLeft", "null", "null", "null", "null", "null", "doorRight" },
        { "null", "null", "null", "null", "null", "null", "null" },
        { "null", "null", "null", "null", "null", "null", "null" },
        { "null", "null", "null", "doorDown", "null", "null", "null" },
    };

    public MapBuilderBuild(int wigth, int hight, float cellSize, Tilemap grid, Tile block, Tile door)
    {
        this.wigth = wigth;
        this.higth = hight;
        this.cellSize = cellSize;

        gridArray = new int[wigth, hight];

        for (int y = 0; y < gridArray.GetLength(0); y++)
        {
            for (int x = 0; x < gridArray.GetLength(1); x++)
            {
                if ((x==0 && y==3) || (x==3 && y==0) || (x==3 && y==6)|| (x==6 && y==3))
                {
                    grid.SetTile(new Vector3Int(x, y, 0), door);
                }
                else
                {
                    grid.SetTile(new Vector3Int(x, y, 0), block);
                }
            }
        }
        
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }

    public void SetValue(Vector3 worldPosition, Tilemap grid, Tile block, Dictionary<Tile, string> mapTile)
    {
        int x ,y;
        GetXY(worldPosition, out x, out y);
        if (x >= 0 && y >= 0 && x < wigth && y < higth && !((x==0 && y==3) || (x==3 && y==0) || (x==3 && y==6)|| (x==6 && y==3)))
        {
            grid.SetTile(new Vector3Int(x, y, 0), block);
            //Debug.Log(y + "" + x);

            y = (y - 6)*-1;    
            
            //Debug.Log(y + "" + x);
            buildMap[y, x] = mapTile[block];
        }
    }

    public void rightClick()
    {
        //Debug.Log("in");
        string[,,] roomBuild = LoadRoomBuild();
        Debug.Log(roomBuild.GetLength(0));
        roomBuild = AddBuildMapToRoomBuild(roomBuild, buildMap);
        Debug.Log(roomBuild.GetLength(0));
        SaveRoomBuild(roomBuild);
        

        /*
        for (int x = 0; x < roomBuild.GetLength(1); x++)
        {
            string msg = " ";
            for (int y = 0; y < roomBuild.GetLength(1); y++)
            {
                msg += " - " + roomBuild[2,x, y];
            }

            Debug.Log(x +": "+msg);
        }
         */
    }
    
    public void printArray(string[,] array)
    { 
        //Der array wird in die Console geschrieben (du hättest auch einfach den namen der methode lesen können)
        for (int x = 0; x < array.GetLength(0); x++)
        {
            string msg = " ";
            for (int y = 0; y < array.GetLength(0); y++)
            {
                msg += " - " + array[x, y];
            }

            Debug.Log(x +": "+msg);
        }

    }


    public string[,,] AddBuildMapToRoomBuild(string[,,] roomBuild, string[,] buildMap)
    {
        string[,,] newRoomBuild = new string[roomBuild.GetLength(0)+1, roomBuild.GetLength(1), roomBuild.GetLength(2)];
        Debug.Log("In-1");
        for (int i = 0; i < newRoomBuild.GetLength(0); i++)
        {
            Debug.Log("In-I:"+i);
            for (int j = 0; j < newRoomBuild.GetLength(1); j++)
            {
                for (int k = 0; k < newRoomBuild.GetLength(2); k++)
                {
                    Debug.Log("In-2");
                    if (newRoomBuild.GetLength(0) == i+1)
                    {
                        Debug.Log("In-3");
                        newRoomBuild[i, j, k] = buildMap[j, k];   
                    }
                    else
                    {
                        newRoomBuild[i, j, k] = roomBuild[i, j, k];   
                    }

                    /*
                     * if (roomBuild[i, j, k] == "null")
                        newRoomBuild[i, j, k] = buildMap[j, k];
                    else
                        newRoomBuild[i, j, k] = roomBuild[i, j, k];
                     */
                }
            }
        }
        Debug.Log("In-4");
        string jsonString = JsonConvert.SerializeObject(newRoomBuild);
        Debug.Log(jsonString);
        
        return newRoomBuild;
    }
    
    public void SaveRoomBuild(string[,,] roomBuild)
    {
        string jsonString = JsonConvert.SerializeObject(roomBuild);
        PlayerPrefs.SetString("firstJason", jsonString);

    }
    
    public string[,,] LoadRoomBuild()
    {
        string jsonString = PlayerPrefs.GetString("firstJason");
        return JsonConvert.DeserializeObject<string[,,]>(jsonString);
    }

}