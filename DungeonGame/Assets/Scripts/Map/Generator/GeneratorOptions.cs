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
using UnityEngine.Serialization;

public class GeneratorOptions : MonoBehaviour
{
    [Header("Options - Game")]
    public int layerLevel = 1;

    [Header("Tilemap")]
    public Tilemap gridFloor;
    public Tilemap gridWall;
        
    [Header("Tiles")]
    public Tile fGrass;
    public Tile wTop;
    public Tile wBottom;
    public Tile wCorner;
    public Tile test;
    public Tile spawnPoint;
        
    [Header("GameObjects")]
    public GameObject doorFrame;
    public GameObject doorClosed;
    public GameObject doorOpen;
    public GameObject doorBackground;
    
    [Header("Options - GenerateLayer")]
    public int layerArrayLength = 11;
    public int branch1Value = 1;
    public int branch2Value = 2;
    public int branch3Value = 3;
    public int branch4Value = 4;
    public int spawnRoom = 10;
    public int bossRoom = 11;
    
    [Header("Options - GenerateGrid")]
    public int baseOffset = 2;
    private void Start()
    {
    }
    
    private Dictionary<Tile, string> mapTile = new Dictionary<Tile, string>();
    
    bool tilesFilled = false;
    private void TileLinkedList()
    {
        tilesFilled = true;
        //Alle Tiles die es zur verfügung gibt
        mapTile.Add(fGrass, "fGrass");
        mapTile.Add(wTop, "wTop");
        mapTile.Add(wBottom, "wBottom");
        mapTile.Add(wCorner, "wCorner");
        mapTile.Add(test, "test");
        mapTile.Add(spawnPoint, "spawnPoint");
        
    }
    
    public Dictionary<Tile, string> GetTileLinkedList()
    {
        if (tilesFilled == false)
        {
            TileLinkedList();   
        }
        return mapTile;
    }
    
    
    
}
