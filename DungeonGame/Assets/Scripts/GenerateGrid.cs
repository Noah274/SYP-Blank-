using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;


public class GenerateGrid : MonoBehaviour
{
    public GenerateLayer script;
    public System.Random _random = new System.Random();
    private int[,] mapArray = GenerateLayer.mapArray;
    private int mapArrayLength = GenerateLayer.mapArrayLength;

    private string[,,] roomBuild;
    //LoadRoomBuild()
    //MapsArray.roomBuild
    
    public Tilemap gridFloor;
    public Tilemap gridWall;
    
    private Dictionary<Tile, string> mapTile = new Dictionary<Tile, string>();
    public Tile wall;
    public Tile dirt;
    public Tile grass;
    public Tile gold;


    void Start()
    {
        tileLinkedList();
        roomBuild = LoadRoomBuild();
        //Debug.Log(roomBuild.GetLength(0));
        //string jsonString = PlayerPrefs.GetString("firstJason");
        //Debug.Log(jsonString);
        
        
        script.makeRooms();
        //printArray(mapArray);
        BuildRoom();
    }

    public void tileLinkedList()
    {
        //Alle Tiles die es zur verfügung gibt
        mapTile.Add(wall, "wall");
        mapTile.Add(dirt, "dirt");
        mapTile.Add(grass, "grass");
        mapTile.Add(gold, "gold");

    }

    public Tile GetTileByNum(int num)
    {
        //Gibt den ersten wert von mapTile aus
        //(Ich habe keine bessere methode gefunden)
        int count = 0;
        foreach(KeyValuePair<Tile,string> pair in mapTile)
        {
            if(count == num)
            {
                return pair.Key;
            }

            count++;
        }
        return null;
    }
    

    public void printArray(int[,] array)
    { 
        //Der array wird in die Console geschrieben (du hättest auch einfach den namen der methode lesen können)
        for (int x = 0; x < mapArrayLength; x++)
        {
            string msg = " ";
            for (int y = 0; y < mapArrayLength; y++)
            {
                msg += " - " + array[x, y];
            }

            Debug.Log(x +": "+msg);
        }

    }
    
    
    public int[] chooseRoom(int mapArrayPosX, int mapArrayPosY)
    {
        int[] values = new int[2];
        
        //Die Raum Art und der Raum wird ausgewählt
        if (mapArray[mapArrayPosX, mapArrayPosY] == GenerateLayer.spawnRoom)
        {
            values[1] = 2;
            values[0] = _random.Next(0, MapsArray.spawnBuild.GetLength(0)); 
        }
        else if (mapArray[mapArrayPosX, mapArrayPosY] == GenerateLayer.bossRoom)
        {
            values[1] = 1;
            values[0] = _random.Next(0, MapsArray.bossBuild.GetLength(0)); 
        }
        else
        {
            values[0] = _random.Next(0, roomBuild.GetLength(0)); 
        }

        return values;
    }

    public void BuildRoom()
    {
        //Debug.Log("-BuildRoom");
        int arrayX = 0;
        int arrayY = 0;
        int boxLength = roomBuild.GetLength(2);
        int arrayLength = 0;
        
        //Das Array mit dem Layer wird durchgegangen
        for (int mapArrayPosY = 0; mapArrayPosY < mapArrayLength; mapArrayPosY++)
        {
            for (int mapArrayPosX = 0; mapArrayPosX < mapArrayLength; mapArrayPosX++)
            {
                //Debug.Log("A:"+a+" - B:"+b);
                //Es wird überprüft, ob ein Raum gemacht werden soll
                if (mapArray[mapArrayPosX, mapArrayPosY] != 0)
                {
                    int[] roomtype = chooseRoom(mapArrayPosX, mapArrayPosY);
                    
                    //Die liste (wie viele Map vorlagen es gibt), kann unterschiedlich lange sein
                    switch (roomtype[1])
                    {
                        case 0:
                            arrayLength = roomBuild.GetLength(0);
                            break;
                        case 1:
                            arrayLength = MapsArray.bossBuild.GetLength(0);
                            break;
                        case 2:
                            arrayLength = MapsArray.spawnBuild.GetLength(0);
                            break;
                    }
                    
                    //Die richtige Vorlage wird gefunden 
                    for (int arrayNum = 0; arrayNum < arrayLength; arrayNum++)
                    {
                        if (arrayNum == roomtype[0])
                        {
                            //Die Vorlage wird platziert
                            for (int y = 0; y < boxLength; y++)
                            {
                                for (int x = 0; x < boxLength; x++)
                                {
                                    printMap(arrayNum, y, x, arrayY, arrayX, mapArrayPosX, mapArrayPosY, roomtype[1]);
                                }
                            }

                            arrayX += boxLength;
                        }
                    }  
                }
                else
                {
                    arrayX += boxLength;
                }
            }
            arrayY += boxLength;
            arrayX = 0;
        }
    }
    
    public void printMap(int arrayNum, int y, int x, int arrayY, int arrayX, int mapArrayPosX, int mapArrayPosY, int roomType)
    {
        //Auswahl, welche Art von raum es ist
        string tileBlock = "";
        switch (roomType)
        {
            case 0:
                tileBlock = roomBuild[arrayNum, x, y];
                break;
            case 1:
                tileBlock = MapsArray.bossBuild[arrayNum, x, y];
                break;
            case 2:
                tileBlock = MapsArray.spawnBuild[arrayNum, x, y];
                break;
        }
        
        int outputTile = 0;
        bool output = false;
        
        //Auswahl, auf welcher seite die Tür Platziert werden muss
        switch (tileBlock)
        {
            case "doorUp":
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX - 1, mapArrayPosY);
                break;
            case "doorDown":
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX + 1, mapArrayPosY);
                break;
            case "doorRight":
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX, mapArrayPosY - 1);
                break;
            case "doorLeft":
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX, mapArrayPosY + 1);
                break;
        }
        
        if (output)
        {
            outputTile = 2;
        }
        else
        {
            outputTile = 0;
        }
        
        //Es wird geschaut, welches Tile platziert werden muss
        int count = 0;
        foreach (KeyValuePair<Tile, string> combi in mapTile)
        {
            //Debug.Log("Value:" + combi.Value);
            if (tileBlock ==  combi.Value)
            {
                outputTile = count;
            }

            count++;
        }
        
        //Tile wird platziert
        gridFloor.SetTile(new Vector3Int(x + arrayX, y- arrayY),  GetTileByNum(outputTile));
    }

    private bool makeDoor(int mapArrayPosX, int mapArrayPosY, int posX, int posY)
    {
        //hier wird entschieden ob eine Tür ist oder nicht
        int temp = mapArray[mapArrayPosX, mapArrayPosY];
        if (script.IsPositionInArray(mapArray, posX, posY) == false )
        {
            return false;
        }

        if (((mapArray[posX, posY] == temp || mapArray[posX, posY] == GenerateLayer.spawnRoom)&& temp != 0)  || (temp == GenerateLayer.spawnRoom && mapArray[posX, posY] != 0)) 
        {
            return true;
        }
        return false;
    }
    
    public string[,,] LoadRoomBuild()
    {
        string jsonString = PlayerPrefs.GetString("firstJason");
        return JsonConvert.DeserializeObject<string[,,]>(jsonString);
    }
}
