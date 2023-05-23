using UnityEngine;
using UnityEngine.Tilemaps;

public class ReadTilesInArea : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int bottomLeft;
    public Vector3Int topRight;

    private TileBase[,] tilesArray;

    void Start()
    {
        int width = topRight.x - bottomLeft.x + 1;
        int height = topRight.y - bottomLeft.y + 1;
        
        //int width = (-14) - (-18) + 1;
        //int height = 2 - (-1) + 1;

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
        
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.Log("Tile at (" + (x + bottomLeft.x) + ", " + (y + bottomLeft.y) + "): " + tilesArray[x, y]);
            }
        }
    }
}