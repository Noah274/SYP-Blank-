using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class ReadTilesInArea : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int bottomLeft;
    public Vector3Int topRight;

    private TileBase[,] tilesArray;
    
    public GameObject[] areaScan;
    

    void Start()
    {
        int width = topRight.x - bottomLeft.x + 1;
        int height = topRight.y - bottomLeft.y + 1;

        tilesArray = new TileBase[width, height];

        for (int x = bottomLeft.x; x <= topRight.x; x++)
        {
            for (int y = bottomLeft.y; y <= topRight.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);
                tilesArray[x - bottomLeft.x, y - bottomLeft.y] = tile;
            }
        }

        tileCount();

        // serialize the tilesArray as a JSON string
        string json = "{ \"Tiles\": [";
        for (int x = 0; x < tilesArray.GetLength(0); x++)
        {
            for (int y = 0; y < tilesArray.GetLength(1); y++)
            {
                TileBase tile = tilesArray[x, y];
                string tileName = (tile != null) ? tile.name : "null";
                json += "\"" + tileName + "\",";
            }
        }
        json = json.TrimEnd(',') + "]}";
        
        Debug.Log("JSON string: " + json);

        // write the JSON string to a file
        string filePath = Path.Combine(Application.dataPath, "SavedRooms_.json");
        Debug.Log("File path: " + filePath);
        File.WriteAllText(filePath, json);
    }

    private void tileCount()
    {
        for (int x = bottomLeft.x; x <= topRight.x; x++)
        {
            for (int y = bottomLeft.y; y <= topRight.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);
                tilesArray[x - bottomLeft.x, y - bottomLeft.y] = tile;
            }
        }

        Debug.Log("Number of tiles: " + tilesArray.Length);
    }
}