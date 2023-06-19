using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using Newtonsoft.Json;

public class scan_Square : MonoBehaviour
{

    public System.Random _random = new System.Random();
    
    public Tilemap tilemap;
    private string[,] tilesArray;
    void Start()
    {
        // GameObject "Create Empty" einlesen
        GameObject createEmptyObject = GameObject.Find("NormalRoom");

        if (createEmptyObject != null)
        {
            // Alle Kinder-Objekte unter dem "Create Empty" GameObject einlesen
            Transform[] childTransforms = createEmptyObject.GetComponentsInChildren<Transform>();

            // Für jedes Kind-Objekt prüfen, ob es ein Square ist und Eckpunkte berechnen
            foreach (Transform childTransform in childTransforms)
            {
                if (childTransform.CompareTag("Square"))
                {
                    // Eckpunkte berechnen
                    Vector3[] corners = CalculateCornerPoints(childTransform.gameObject);

                    //Debug.Log("Bottom Left: " + corners[0]);
                    //Debug.Log("Top Right: " + corners[3]);

                    saveRoomBuild(corners);
                }
            }
        }
        else
        {
            Debug.LogError("Create Empty GameObject not found!");
        }
    }

    Vector3[] CalculateCornerPoints(GameObject squareObject)
    {
        // Größe des GameObjects ermitteln
        Vector3 size = squareObject.GetComponent<Renderer>().bounds.size;

        // Eckpunkte berechnen
        Vector3[] corners = new Vector3[4];
        corners[0] = squareObject.transform.position + new Vector3(-size.x / 2, -size.y / 2, 0);
        corners[1] = squareObject.transform.position + new Vector3(-size.x / 2, size.y / 2, 0);
        corners[2] = squareObject.transform.position + new Vector3(size.x / 2, -size.y / 2, 0);
        corners[3] = squareObject.transform.position + new Vector3(size.x / 2, size.y / 2, 0);

        return corners;
    }

    void saveRoomBuild(Vector3[] corners)
    {
        int width = roundToInt(corners[3].x) - roundToInt(corners[0].x) + 1;
        int height = roundToInt(corners[3].y) - roundToInt(corners[0].y) + 1;

        tilesArray = new string[width, height];
        for (int x = roundToInt(corners[0].x); x <= roundToInt(corners[3].x); x++)
        {
            for (int y = roundToInt(corners[0].y); y <= roundToInt(corners[3].y); y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);
                string tileName = (tile != null) ? tile.name : "null";
                tilesArray[x - roundToInt(corners[0].x), y - roundToInt(corners[0].y)] = tileName;
            }
        }
        
    


        //tileCount(corners);

        string[,,] roomBuild =
        {
            //y
            {
                //x
                {"fGrass", "fGrass", "fGrass", "fGrass", "fGrass"},
                {"fGrass", "fGrass", "test", "fGrass", "fGrass"},
                {"fGrass", "test", "test", "fGrass", "fGrass"},
                {"fGrass", "fGrass", "fGrass", "fGrass", "fGrass"},
                {"fGrass", "fGrass", "fGrass", "fGrass", "fGrass"},
            }

        };
        
        string[,] newArray = new string[,]
        {
            {"fGrass", "fGrass", "fGrass", "fGrass", "fGrass"},
            {"fGrass", "fGrass", "test", "fGrass", "fGrass"},
            {"fGrass", "test", "test", "test", "fGrass"},
            {"fGrass", "fGrass", "test", "fGrass", "fGrass"},
            {"fGrass", "fGrass", "fGrass", "fGrass", "fGrass"}
        };
        
        arrayAdd(ref roomBuild, newArray);
        
        //linked list 
        
        SaveArrayToJson(roomBuild, "SavedRooms_NormalRoom.json");

    }
    
    private void SaveArrayToJson(string[,,] array, string fileName)
    {
        array = ReverseRoomBuild(array);
        array = circledArray(array);
        
        
      
        for (int y = 0; y < array.GetLength(0); y++)
        {

            for (int x = 0; x < array.GetLength(1); x++)
            {
                string row = "";
                for (int z = 0; z < array.GetLength(2); z++)
                {
                    row += array[y, x, z] + " ";
                }
                Debug.Log("row " + x + ": " + row);
            }
            
        }
        
        string jsonString = JsonConvert.SerializeObject(array);
        PlayerPrefs.SetString(fileName, jsonString);
    }
    
    public string[,,] ReverseRoomBuild(string[,,] roomBuild)
    {
        Debug.Log("ReverseRoomBuild");
        int lengthZ = roomBuild.GetLength(0); // Anzahl der Elemente entlang der y-Achse
        int lengthY = roomBuild.GetLength(1); // Anzahl der Elemente entlang der y-Achse
        int lengthX = roomBuild.GetLength(2); // Anzahl der Elemente entlang der x-Achse

        string[,,] reversedRoomBuild = new string[1, lengthY, lengthX]; // Neuer Array zum Speichern des umgekehrten roomBuild

        for (int z = 0; z < lengthZ; z++)
        {
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    reversedRoomBuild[z, lengthY - 1 - y, x] = roomBuild[z, y, x]; // Vertausche die Elemente entlang der y-Achse
                }
            }
        }

        return reversedRoomBuild;
    }
    
    public string[,,] circledArray(string[,,] array)
    {
        int depth = array.GetLength(0);
        int rows = array.GetLength(1);
        int cols = array.GetLength(2);

        // Erstellen eines neuen Arrays mit größerer Dimension
        string[,,] umrundetesArray = new string[depth, rows + 2, cols + 2];

        int doorUpPlaced = calcDoorPlace(cols);
        int doorDownPlaced = calcDoorPlace(cols);
        int doorLeftPlaced = calcDoorPlace(rows);
        int doorRightPlaced = calcDoorPlace(rows);

        Debug.Log(doorUpPlaced + " " + doorDownPlaced + " " + doorLeftPlaced + " " + doorRightPlaced);
        
        int doorUpCount = 0;
        int doorDownCount = 0;
        int doorLeftCount = 0;
        int doorRightCount = 0;
        
        // Umrundung des Arrays mit Schlagwörtern
        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < rows + 2; y++)
            {
                for (int x = 0; x < cols + 2; x++)
                {
                    if (y == 0 && x == 0)
                    {
                        umrundetesArray[z, y, x] = "wUpLeft";
                    }
                    else if (y == 0 && x == cols + 1)
                    {
                        umrundetesArray[z, y, x] = "wUpRight";
                    }
                    else if (y == rows + 1 && x == 0)
                    {
                        umrundetesArray[z, y, x] = "wDownLeft";
                    }
                    else if (y == rows + 1 && x == cols + 1)
                    {
                        umrundetesArray[z, y, x] = "wDownRight";
                    }
                    else if (y == 0)
                    {
                        
                        if (doorUpCount == doorUpPlaced)
                        {
                            umrundetesArray[z, y, x] = "dUp";
                        }
                        else
                        {
                            umrundetesArray[z, y, x] = "wUp";   
                        }

                        doorUpCount++;
                    }
                    else if (y == rows + 1)
                    {

                        if (doorDownCount == doorDownPlaced)
                        {
                            umrundetesArray[z, y, x] = "dDown";
                        }
                        else
                        {
                            umrundetesArray[z, y, x] = "wDown";   
                        }

                        doorDownCount++;
                    }
                    else if (x == 0)
                    {

                        if (doorLeftCount == doorLeftPlaced)
                        {
                            umrundetesArray[z, y, x] = "dLeft";
                        }
                        else
                        {
                            umrundetesArray[z, y, x] = "wLeft";   
                        }

                        doorLeftCount++;
                    }
                    else if (x == cols + 1)
                    {

                        if (doorRightCount == doorRightPlaced)
                        {
                            umrundetesArray[z, y, x] = "dRight";
                        }
                        else
                        {
                            umrundetesArray[z, y, x] = "wRight";   
                        }

                        doorRightCount++;
                    }
                    else
                    {
                        umrundetesArray[z, y, x] = array[z, y - 1, x - 1];
                    }
                }
            }
        }

        // Rückgabe des umrundeten Arrays
        return umrundetesArray;
    }
    
    void arrayAdd(ref string[,,] array, string[,] newArray)
    {
        int oldDepth = array.GetLength(0);
        int oldRows = array.GetLength(1);
        int oldColumns = array.GetLength(2);

        int newRows = newArray.GetLength(0);
        int newColumns = newArray.GetLength(1);

        string[,,] resultArray = new string[oldDepth + 1, oldRows, oldColumns];

        for (int d = 0; d < oldDepth; d++)
        {
            for (int y = 0; y < oldRows; y++)
            {
                for (int x = 0; x < oldColumns; x++)
                {
                    resultArray[d, y, x] = array[d, y, x];
                }
            }
        }

        for (int y = 0; y < newRows; y++)
        {
            for (int x = 0; x < newColumns; x++)
            {
                resultArray[oldDepth, y, x] = newArray[y, x];
            }
        }

        array = resultArray;
    }

    private int calcDoorPlace(int number)
    {
        number = _random.Next(1, number-1);
        return number;
    }
    
    private int roundToInt(float value)
    {
        return Mathf.RoundToInt(value);
    }
}