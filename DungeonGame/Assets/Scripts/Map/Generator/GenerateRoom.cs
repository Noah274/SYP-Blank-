using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateRoom
{
    private System.Random _random = new System.Random();
    private GeneratorOptions gOptions;

    private int roomId;//X
    private int roomType;
    private string[,] roomArrayBuild;
    private bool roomDone;
    
    private int layerPosX;
    private int layerPosY;
    
    //doors
    private int[] doorDirections = new int[4];
    
    


    public GenerateRoom(GeneratorOptions gOptions, int roomType, int layerPosX, int layerPosY, int roomId, int[] doors)
    {
        this.gOptions = gOptions;
        this.roomType = roomType;
        this.layerPosX = layerPosX;
        this.layerPosY = layerPosY;
        this.roomId = roomId;
        this.roomDone = true;

        this.doorDirections[0] = doors[0]; //dUp;
        this.doorDirections[1] = doors[1]; //dDown;
        this.doorDirections[2] = doors[2]; //dLeft;
        this.doorDirections[3] = doors[3]; //dRight;
        
        //Debug.Log(roomNumber);
        //Debug.Log(doorDirections[0] + " " + doorDirections[1] + " " + doorDirections[2] + " " + doorDirections[3]);
        
        CreateRoom();
    }
    
    private void CreateRoom()
    {
        int selectRoom = 0;
        string[,,] roomArray = null;
        

        if (roomType == gOptions.bossRoom)
        {
            selectRoom = _random.Next(0, MapsArray.bossBuild.GetLength(0)); 
            roomArray = MapsArray.bossBuild;
        }
        else  if (roomType == gOptions.spawnRoom)
        {
            selectRoom = _random.Next(0, MapsArray.spawnBuild.GetLength(0)); 
            roomArray = MapsArray.spawnBuild;
        }
        else
        {
            selectRoom = _random.Next(0, MapsArray.roomBuild.GetLength(0)); 
            roomArray = MapsArray.roomBuild;
        }
        
        SaveArrayRoom(selectRoom, roomArray);
    }

    private void SaveArrayRoom(int selectRoom, string[,,] roomArray)
    {
        int arrayLengthY = roomArray.GetLength(1);
        int arrayLengthX = roomArray.GetLength(2);
        roomArrayBuild = new string[arrayLengthY, arrayLengthX];

        for (int y = 0; y < arrayLengthY; y++)
        {
            for (int x = 0; x < arrayLengthX; x++)
            {
                roomArrayBuild[y, x] = roomArray[selectRoom, y, x];
            }
        }
    }
    public void SetRoomDone()
    {
        roomDone = true;
    }
    
    public bool GetRoomDone()
    {
        return roomDone;
    }
    
    public int GetDUp()
    {
        return doorDirections[0];
    }
    public int GetDDown()
    {
        return doorDirections[1];
    }
    public int GetDLeft()
    {
        return doorDirections[2];
    }
    public int GetDRight()
    {
        return doorDirections[3];
    }
    
    public int GetRoomType()
    {
        return roomType;
    }
    
    public string[,] GetRoomArrayBuild()
    {
        return roomArrayBuild;
    }
    
    public int GetLayerPosX()
    {
        return layerPosX;
    }
    
    public int GetLayerPosY()
    {
        return layerPosY;
    }
    
    public int GetRoomId()
    {
        return roomId;
    }
}
