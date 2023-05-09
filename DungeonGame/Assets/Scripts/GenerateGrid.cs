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

    private string[,,] roomBuild = MapsArray.roomBuild;
    //LoadRoomBuild()
    //MapsArray.roomBuild
    
    public Tilemap gridFloor;
    public Tilemap gridWall;
    public GameObject doorFrame;
    public GameObject doorClosed;
    
    private Dictionary<Tile, string> mapTile = new Dictionary<Tile, string>();
    public Tile fGrass;
    public Tile wTop;
    public Tile wBottom;
    public Tile wCorner;
    public Tile test;
    
    void Start()
    {
        tileLinkedList();
        //roomBuild = LoadRoomBuild();
        //Debug.Log(roomBuild.GetLength(0));
        //string jsonString = PlayerPrefs.GetString("firstJason");
        //Debug.Log(jsonString);
        
        
        script.makeRooms();
        printArray(mapArray);
        BuildRoom();
    }

    public void tileLinkedList()
    {
        //Alle Tiles die es zur verfügung gibt
        mapTile.Add(fGrass, "fGrass");
        mapTile.Add(wTop, "wTop");
        mapTile.Add(wBottom, "wBottom");
        mapTile.Add(wCorner, "wCorner");
        mapTile.Add(test, "test");
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
                msg += " - " + array[y, x];
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
        int distanceX = 0;
        int distanceY = 0;
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
                                    printMap(arrayNum, y, x, distanceY, distanceX, mapArrayPosX, mapArrayPosY, roomtype[1]);
                                }
                            }

                            distanceX += boxLength + 3;
                        }
                    }  
                }
                else
                {
                    distanceX += boxLength + 3;
                }
            }
            distanceY += boxLength + 3;
            distanceX = 0;
        }
    }
    
    /*
        wUp
        wLeft
        wRight
        wDown
        wCorners
        
        dUp
        dRight
        dLeft
        dDown
        
        fGrass
     */
    
    public void printMap(int arrayNum, int y, int x, int distanceY, int distanceX, int mapArrayPosX, int mapArrayPosY, int roomType)
    {
        //Auswahl, welche Art von raum es ist
        string tileBlock = "";
        switch (roomType)
        {
            case 0:
                tileBlock = roomBuild[arrayNum, y, x];
                break;
            case 1:
                tileBlock = MapsArray.bossBuild[arrayNum, y, x];
                break;
            case 2:
                tileBlock = MapsArray.spawnBuild[arrayNum, y, x];
                break;
        }
        
        int outputTile = 0;
        bool output = false;
        bool tile = true;
        
        //Auswahl, auf welcher seite die Tür Platziert werden muss
        
        switch (tileBlock)
        {
            case "dUp":
                //Unten
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX, mapArrayPosY + 1);
                if (output)
                {
                    placeGameObject(doorFrame, x + distanceX, y- distanceY+1, 180f);
                    placeGameObject(doorClosed, x + distanceX, y- distanceY+1, 180f); 
                }
                else
                {
                    tile = false;
                }
                break;
            case "dDown":
                //Oben
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX, mapArrayPosY - 1);
                if (output)
                {
                    placeGameObject(doorFrame, x + distanceX, y- distanceY, 0f);
                    placeGameObject(doorClosed, x + distanceX, y- distanceY, 0f);
                }
                else
                {
                    tile = false;
                }
                break;
            case "dRight":
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX + 1, mapArrayPosY);
                if (output)
                {
                    placeGameObject(doorFrame, x + distanceX-0.5f, y- distanceY+0.5f, -90f);
                    placeGameObject(doorClosed, x + distanceX-0.5f, y- distanceY+0.5f, -90f); 
                }
                else
                {
                    tile = false;
                }
                break;
            case "dLeft":
                output = makeDoor(mapArrayPosX, mapArrayPosY, mapArrayPosX - 1, mapArrayPosY);
                if (output)
                {
                    placeGameObject(doorFrame, x + distanceX+0.5f, y- distanceY+0.5f, 90f);
                    placeGameObject(doorClosed, x + distanceX+0.5f, y- distanceY+0.5f, 90f);
                }
                else
                {
                    tile = false;
                }
                break;
        }

        
        if (tileBlock == "wUp" || (tileBlock == "dUp" && tile == false))
        {   
            Debug.Log("test");
            tile = false;
            paceWallOnMAp(x + distanceX, y - distanceY, 0 , 1, 180);
        }
        else if (tileBlock == "wDown" || (tileBlock == "dDown" && tile == false))
        {   
            Debug.Log("test");
            tile = false;
            paceWallOnMAp(x + distanceX, y - distanceY, 0 , -1, 0);
        }
        else if (tileBlock == "wRight" || (tileBlock == "dRight" && tile == false))
        {   
            Debug.Log("test");
            tile = false;
            paceWallOnMAp(x + distanceX, y - distanceY, 1 , 0, -90);
        }
        else if (tileBlock == "wLeft" || (tileBlock == "dLeft" && tile == false))
        {
            Debug.Log("test");
            tile = false;
            paceWallOnMAp(x + distanceX, y - distanceY, -1 , 0, 90);
        }
        
        switch (tileBlock)
        {
            case "wUpLeft":
                tile = false;
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX-1, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY-1), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX-1, y- distanceY-1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
            case "wUpRight":
                tile = false;
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX+1, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY-1), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX+1, y- distanceY-1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
            case "wDownLeft":
                tile = false;
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX-1, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY+1), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX-1, y- distanceY+1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
            case "wDownRight":
                tile = false;
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX+1, y- distanceY), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY+1), GetTileByNum(getNumOfTile("wCorner")));  
                gridFloor.SetTile(new Vector3Int(x + distanceX+1, y- distanceY+1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
        }
        

        //Tile wird platziert
        if (tile)
        {
            gridFloor.SetTile(new Vector3Int(x + distanceX, y- distanceY), GetTileByNum(getNumOfTile(tileBlock)));   
        }
    }

    private void placeGameObject(GameObject o, float distanceX, float distanceY, float f)
    {
        Instantiate(o, new Vector3(distanceX , distanceY), Quaternion.Euler(0, 0, f));
    }

    private void paceWallOnMAp(int x, int y, int secondX, int secondY, float rotation)
    {
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rotation), Vector3.one);
        
        gridFloor.SetTile(new Vector3Int(x , y), GetTileByNum(getNumOfTile("wBottom")));
        gridFloor.SetTransformMatrix(new Vector3Int(x , y), matrix);  
        
        gridFloor.SetTile(new Vector3Int(x + secondX, y- secondY), GetTileByNum(getNumOfTile("wTop")));
        gridFloor.SetTransformMatrix(new Vector3Int(x + secondX, y- secondY), matrix);  
    }

    private int getNumOfTile(string tileBlock)
    {
        //Es wird geschaut, welches Tile platziert werden muss
        int outputTile = 0;
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

        return outputTile;
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
