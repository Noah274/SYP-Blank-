using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;
using Newtonsoft.Json;

public class MapBuilderData : MonoBehaviour
{
    private Dictionary<Tile, string> mapTile = new Dictionary<Tile, string>();
    private int count = 0;
    private MapBuilderBuild grid;
    public Tilemap map;
    public Tile defaultBlock;
    public Tile door;
    public Tile wall;
    public Tile dirt;
    public Tile grass;
    public Tile gold;
    
    private void Start()
    {
        tileLinkedList();

        int boxLength = 7;
        grid = new MapBuilderBuild(boxLength, boxLength, 1f, map, defaultBlock, door);

        string[,,] test = LoadRoomBuild();
        Debug.Log(test.GetLength(0));

    }
    
    public string[,,] LoadRoomBuild()
    {
        string jsonString = PlayerPrefs.GetString("firstJason");
        return JsonConvert.DeserializeObject<string[,,]>(jsonString);
    }

    public void tileLinkedList()
    {
        
        mapTile.Add(wall, "wall");
        mapTile.Add(dirt, "dirt");
        mapTile.Add(grass, "grass");
        mapTile.Add(gold, "gold");
    }
    
    private void Update()
    {
        bool change = true;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            count--;
            if (allowedToCount(count))
            {
                change = true;   
            }
            else
            {
                count++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            count++;
            if (allowedToCount(count))
            {
                change = true;   
            }
            else
            {
                count--;
            }
        }
        
        if (change)
        {
            map.SetTile(new Vector3Int(-2, 0, 0), GetTileByNum(count));
            map.SetTile(new Vector3Int(-2, -1, 0), GetTileByNum(count));
            map.SetTile(new Vector3Int(-3, 0, 0), GetTileByNum(count));
            map.SetTile(new Vector3Int(-3, -1, 0), GetTileByNum(count));
        }
        change = false;
        
        if (Input.GetMouseButtonDown(0)) {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), map, GetTileByNum(count), mapTile);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            grid.rightClick();
        }
        
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Delete-firstJason oder so");
            PlayerPrefs.DeleteKey("firstJason");
            //string jsonString = PlayerPrefs.GetString("firstJason");
            //Debug.Log(jsonString);
        }
        
    }
    
    
    public bool allowedToCount(int count)
    {
        if (count >= 0 && count <= mapTile.Count-1)
        {
            return true;
        }

        return false;
    }
    
    public Tile GetTileByNum(int num)
    {
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

}