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
using UnityEngine.UI;

public class GeneratorOptions : MonoBehaviour
{
    [Header("Options - Game")]
    public int layerLevel = 1;
    
    [Header("Enemy - Spawning")]
    public GameObject[] enemy;
    public GameObject bossEnemy;
    public int spawnRange;
    public int spawnDelay;
    public float damageMultiplier;
    public float healthMultiplier;

    [Header("Tilemap")]
    public Tilemap gridFloor;
    public Tilemap gridWall;
        
    [Header("Tiles")]
    public Tile[] fGrass;
    public Tile fSpawnPoint;
    public Tile[] fStone;
    
    [Header("Tiles - GenerateGrid")]
    public Tile[] wTop;
    public Tile[] wBottom;
    public Tile wCorner;

    [Header("GameObjects")]
    public GameObject doorFrame;
    public GameObject doorClosed;
    public GameObject doorOpen;
    public GameObject[] floorDecorations;
    public GameObject[] lootDecorations;
    public GameObject[] bossFloorDecorations;
    public GameObject[] bossFloorDecorationsPillar;
    
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
    public GameObject doorHitbox;
    
    [Header("Options - nextLayer")]
    public float waitingTime;
    public Text levelText;
    
    
    private void Start()
    {
    }
    
    private Dictionary<Tile, string> mapTile = new Dictionary<Tile, string>();
    
    bool tilesFilled = false;
    private void TileLinkedList()
    {
        tilesFilled = true;
        //Alle Tiles die es zur verfügung gibt
        //mapTile.Add(fGrass, "fGrass");
        //mapTile.Add(wTop, "wTop");
        //mapTile.Add(wBottom, "wBottom");
        mapTile.Add(wCorner, "wCorner");
        mapTile.Add(fSpawnPoint, "spawnPoint");
        
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
