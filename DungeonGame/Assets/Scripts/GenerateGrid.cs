using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateGrid : MonoBehaviour
{
    public Tilemap gridFloor;
    public Tilemap gridWall;
    public Tile wall;
    public Tile dirt;
    public Tile grass;
    public int rooms;


    private int[,,] arrayBuild =
    {
        {
            {1, 1, 2, 1, 1},
            {1, 3, 3, 3, 1},
            {2, 3, 3, 3, 2},
            {1, 3, 3, 3, 1},
            {1, 1, 2, 1, 1},
        }
        /*
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        },
        {
            {1, 1, 1, 1, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 2, 2, 2, 1},
            {1, 1, 1, 1, 1},
        }
        */

    };
    /*private int[] roomTypes =
    {
        1, 
        2
    };
    private Tile[] roomBlockName =
    {
        dirt,
        grass
    };*/

    void Start()
    {
        //BuildRoom();

    }

    public int CooseRoom()
    {
        int num = Random.Range(0, arrayBuild.GetLength(0));
        return num;
    }

    public void BuildRoom()
    {
        int roomDif = 0;
        
        
        for (int i = 0; i < rooms; i++)
        {
            int roomtype = CooseRoom();
            //Debug.Log(roomtype);
            for (int x = 0; x < arrayBuild.GetLength(0); x++)
            {
                if (x == roomtype)
                {
                    for (int y = 0; y < arrayBuild.GetLength(1); y++)
                    {
                        for (int z = 0; z < arrayBuild.GetLength(2); z++)
                        {
                            printMap(x, y, z, roomDif);
                        }
                    }
                    roomDif += arrayBuild.GetLength(2) + 2;
                }
            }
        }
    }


    public void printMap(int x, int y, int z, int roomDif)
    { 
        /*
        for (int i = 0; i < roomTypes.GetLength(0); i++)
        {
            if (arrayBuild[x, y, z] == roomTypes[i])
            {
                //Debug.Log((roomBlockName)i);
                //string type = ((roomBlockName)i);
                //Debug.Log(type);
                //string sype = "grass";
                gridFloor.SetTile(new Vector3Int(y + roomDif, z), roomBlockName[i]);
                //Debug.Log((roomBlockName)i);
            }
        }*/

        switch (arrayBuild[x, y, z])
        {

            case 1:
                gridWall.SetTile(new Vector3Int(y + roomDif, z), wall);
                break;
            case 2:
                gridFloor.SetTile(new Vector3Int(y + roomDif, z), grass);
                break;
            case 3:
                gridFloor.SetTile(new Vector3Int(y + roomDif, z), dirt);
                break;
        }
    }
    
    
}

