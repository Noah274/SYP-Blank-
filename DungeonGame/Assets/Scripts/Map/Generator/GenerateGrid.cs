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


public class GenerateGrid : MonoBehaviour
{
	private GeneratorOptions gOptions;
	private System.Random _random = new System.Random();
	private LinkedList<GenerateRoom> rooms = new LinkedList<GenerateRoom>();


	public void StartGenerationGrid(GeneratorOptions gOptions)
	{

		this.gOptions = gOptions;

		GoThroughLayer();
		PrintLayer();
		
		/*
		GameObject[] gameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject obj in gameObjects)
		{
			if (obj.name == "1")
			{
				SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = gOptions.doorOpen.GetComponent<SpriteRenderer>().sprite;
				
				spriteRenderer.color = Color.red;
				obj.GetComponent<RoomReference>().room.SetRoomDone();
			}
		}*/
	}
	

	private void PrintLayer()
	{

		int offsetX = 0;
		int offsetY = 0;

		Dictionary<int, GenerateRoom> buildPos = new Dictionary<int, GenerateRoom>();
		foreach (var room in rooms)
		{
			int roomPos =  Int32.Parse(room.GetLayerPosX().ToString() + room.GetLayerPosY().ToString());
			buildPos.Add(roomPos, room);
		}
		
		//int[] teleportRooms = new int[4];
		
		int layerLength = gOptions.layerArrayLength;
		//ToDo-problem!!!!!
		int arrayX = 7;
		int arrayY = 7;
		for (int layerPosY = 0; layerPosY < layerLength; layerPosY++)
		{
			for (int layerPosX = 0; layerPosX < layerLength; layerPosX++)
			{
				int pos =  Int32.Parse(layerPosX.ToString() +layerPosY.ToString());
				if(buildPos.ContainsKey(pos))
				{
					GenerateRoom room = buildPos[pos];
					string[,] roomArray = room.GetRoomArrayBuild();
					arrayX = roomArray.GetLength(1);
					arrayY = roomArray.GetLength(0);

					for (int y = 0; y < arrayY; y++)
					{
						for (int x = 0; x < arrayX; x++)
						{
							string tileBlock = roomArray[(arrayY - 1 - y), (x)];
							PlaceTile((x+ offsetX+layerPosX), (y - (offsetY+layerPosY)), tileBlock, room);
						}
					}
					
					offsetX += arrayX + gOptions.baseOffset;
					
					//get ID
					int[] teleportRooms = new int[4];
					if (room.GetDUp() != 0)
					{
						int posOld =  Int32.Parse(layerPosX.ToString() +(layerPosY-1).ToString());
						teleportRooms[0] = buildPos[posOld].GetRoomId();
					}
					if (room.GetDDown() != 0)
					{
						int posOld =  Int32.Parse(layerPosX.ToString() +(layerPosY+1).ToString());
						teleportRooms[1] = buildPos[posOld].GetRoomId();
					}
					if (room.GetDLeft() != 0)
					{
						int posOld =  Int32.Parse((layerPosX - 1).ToString() +(layerPosY).ToString());
						teleportRooms[2] = buildPos[posOld].GetRoomId();
					}
					if (room.GetDRight() != 0)
					{
						int posOld =  Int32.Parse((layerPosX + 1).ToString() +(layerPosY).ToString());
						teleportRooms[3] = buildPos[posOld].GetRoomId();
					}
					room.SetTeleportDirections(teleportRooms);
				}
				else
				{
					offsetX += arrayX + gOptions.baseOffset;
				}
			}
			offsetY += arrayY + gOptions.baseOffset;
			offsetX = 0;
		}
		

	}

	private void PlaceTile(int posX, int posY, string tileBlock, GenerateRoom room)
	{
		bool wallPlaces = false;

        if (tileBlock == "")
        {
	        return;
        }

        switch (tileBlock)
        {
	        case "dUp":
		        int roomTypeUp = IsValidRoom(room, room.GetDUp());
		        if (roomTypeUp == gOptions.bossRoom)
		        {
			        PlaceDoor(posX, posY, 0f, room.GetRoomDone(), true, room);
			        wallPlaces = true;
		        }
		        else if (roomTypeUp == 0)
		        {
			        PlaceDoor(posX, posY, 0f, room.GetRoomDone(), false, room);
			        wallPlaces = true;
		        }
		        break;
	        case "dDown":
		        int roomTypeDown = IsValidRoom(room, room.GetDDown());
		        if (roomTypeDown == gOptions.bossRoom)
		        {
			        PlaceDoor(posX, posY + 1, 180f, room.GetRoomDone(), true, room);
			        wallPlaces = true;
		        }
		        else if (roomTypeDown == 0)
		        {
			        PlaceDoor(posX, posY + 1, 180f, room.GetRoomDone(), false, room);
			        wallPlaces = true;
		        }
		        break;
	        case "dLeft":
		        int roomTypeLeft = IsValidRoom(room, room.GetDLeft());
		        if (roomTypeLeft == gOptions.bossRoom)
		        {
			        PlaceDoor(posX + 0.5f, posY + 0.5f, 90f, room.GetRoomDone(), true, room);
			        wallPlaces = true;
		        }
		        else if (roomTypeLeft == 0)
		        {
			        PlaceDoor(posX + 0.5f, posY + 0.5f, 90f, room.GetRoomDone(), false, room);
			        wallPlaces = true;
		        }
		        break;
	        case "dRight":
		        int roomTypeRight = IsValidRoom(room, room.GetDRight());
		        if (roomTypeRight == gOptions.bossRoom)
		        {
			        PlaceDoor(posX - 0.5f, posY + 0.5f, -90f, room.GetRoomDone(), true, room);
			        wallPlaces = true;
		        }
		        else if (roomTypeRight == 0)
		        {
			        PlaceDoor(posX - 0.5f, posY + 0.5f, -90f, room.GetRoomDone(), false, room);
			        wallPlaces = true;
		        }
		        break;
        }


        
        if (tileBlock == "wUp" || (tileBlock == "dUp" && wallPlaces == false))
        {
	        paceWallOnMAp(posX, posY , 0 , -1, 0);
	        wallPlaces = true;
        }
        else if (tileBlock == "wDown" || (tileBlock == "dDown" && wallPlaces == false))
        {
	        paceWallOnMAp(posX, posY, 0 , 1, 180);
	        wallPlaces = true;
        }
        else if (tileBlock == "wLeft" || (tileBlock == "dLeft" && wallPlaces == false))
        {
	        paceWallOnMAp(posX , posY, -1 , 0, 90);
	        wallPlaces = true;
        }
        else if (tileBlock == "wRight" || (tileBlock == "dRight" && wallPlaces == false))
        {
            paceWallOnMAp(posX , posY, 1 , 0, -90);
            wallPlaces = true;
        }
        
        
        switch (tileBlock)
        {
            case "wUpLeft":
	            wallPlaces = true;
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX-1, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY+1), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX-1, posY+1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
            case "wUpRight":
	            wallPlaces = true;
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX+1, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY+1), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX+1, posY+1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
            case "wDownLeft":
	            wallPlaces = true;
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX-1, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY-1), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX-1, posY-1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
            case "wDownRight":
	            wallPlaces = true;
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX+1, posY), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX, posY-1), GetTileByNum(getNumOfTile("wCorner")));  
                gOptions.gridWall.SetTile(new Vector3Int(posX+1, posY-1), GetTileByNum(getNumOfTile("wCorner")));  
                break;
        }

        if (!wallPlaces)
        {
	        if (tileBlock == "fGrass" || tileBlock == "centerfGrass")
	        {
		        gOptions.gridFloor.SetTile(new Vector3Int(posX, posY), gOptions.fGrass[_random.Next(0, gOptions.fGrass.Length)]);
		        PlaceGrassFloorDecoration(posX, posY, room);
		        PlaceCenterPoint(posX, posY, room, tileBlock);
		        
	        }
	        else  if (tileBlock == "fStone" || tileBlock == "centerfStone")
	        {
		        gOptions.gridFloor.SetTile(new Vector3Int(posX, posY), gOptions.fStone[_random.Next(0, gOptions.fStone.Length)]);
		        PlaceBossFloorDecoration(posX, posY, room);
		        PlaceCenterPoint(posX, posY, room, tileBlock);
	        }
	        else if (tileBlock == "fSpawnPoint")
	        {
		        gOptions.gridFloor.SetTile(new Vector3Int(posX, posY), gOptions.fSpawnPoint);
		        PlaceCenterPoint(posX, posY, room, tileBlock);
		        
		        Vector3 spawnPosition = new Vector3(posX, posY, 0f);
		        GameObject spawnPointPlayer = new GameObject("spawnPointPlayer");
		        spawnPointPlayer.transform.position = spawnPosition;
		        spawnPointPlayer.tag = "spawnPointPlayer";
	        }
	        else
	        {
		        gOptions.gridFloor.SetTile(new Vector3Int(posX, posY), GetTileByNum(getNumOfTile(tileBlock)));       
	        }
        }
	}

	private void PlaceCenterPoint(int posX, int posY, GenerateRoom room, string tileBlock)
	{
		if (tileBlock == "centerfGrass" || tileBlock == "centerfStone"|| tileBlock == "fSpawnPoint")
		{
			Vector3 spawnPosition = new Vector3(posX, posY, 0f);
			GameObject spawnPointPlayer = new GameObject("RoomCenterPoint_" + room.GetRoomId());
			spawnPointPlayer.transform.position = spawnPosition;
			spawnPointPlayer.tag = "RoomCenterPoint";
		}
	}


	private void PlaceGrassFloorDecoration(int posX, int posY, GenerateRoom room)
	{
		int random = _random.Next(0, 8);
		if (random == 1)
		{
			int tileNumber = _random.Next(0, gOptions.floorDecorations.Length);
			GameObject randomDecoration = gOptions.floorDecorations[tileNumber];
		
			random = _random.Next(0, 5);
			if (random == 1)
			{
				tileNumber = _random.Next(0, gOptions.lootDecorations.Length);
				randomDecoration = gOptions.lootDecorations[tileNumber];
			}
			
			Vector3 position = new Vector3(posX, posY, 0f);
			GameObject decorationInstance = Instantiate(randomDecoration, position, Quaternion.identity);

			decorationInstance.transform.position = position;	
		}
	}
	
	private void PlaceBossFloorDecoration(int posX, int posY, GenerateRoom room)
	{
		int random = _random.Next(0, 8);
		if (random == 1)
		{
			int tileNumber = _random.Next(0, gOptions.bossFloorDecorations.Length);
			GameObject randomDecoration = gOptions.bossFloorDecorations[tileNumber];
			Vector3 position = new Vector3(posX, posY, 0f);
			GameObject decorationInstance = Instantiate(randomDecoration, position, Quaternion.identity);

			decorationInstance.transform.position = position;	
		}
	}


	private int IsValidRoom(GenerateRoom room, int direction)
	{
		if (direction == 0 || room.GetRoomType() == gOptions.bossRoom)
		{
			return -1;
		}

		if (direction == gOptions.bossRoom)
		{
			return gOptions.bossRoom;
		}

		if (direction == gOptions.spawnRoom || room.GetRoomType() == gOptions.spawnRoom)
		{
			return 0;
		}
		
		//Chance to place a door
		if (room.GetRoomType() != direction)
		{
			int random = _random.Next(0, 2);
			if (random == 1)
			{
				return 0;
			}
			
			return -1;
		}
		
		return 0;
	}

	private void PlaceDoor(float posX, float posY, float rotation, bool roomDone, bool isBossRoom, GenerateRoom room)
	{
		placeGameObject(gOptions.doorFrame, posX, posY, rotation);
		placeWhitePlate(posX, posY, rotation);
		
		if (roomDone)
		{
			if (isBossRoom)
			{
				placeGameObject(gOptions.doorOpen, posX, posY, rotation, true,room);
			}
			else
			{
				placeGameObject(gOptions.doorOpen, posX, posY, rotation, false, room);	
			}
		}
		else
		{
			if (isBossRoom)
			{
				placeGameObject(gOptions.doorClosed, posX, posY, rotation, true, room);
			}
			else
			{
				placeGameObject(gOptions.doorClosed, posX, posY, rotation, false, room);
			}
		}
		
	}
	

	

	private void placeWhitePlate(float posX, float posY, float rotation)
	{
		Instantiate(gOptions.doorHitbox, new Vector3(posX, posY, 0f), Quaternion.Euler(0f, 0f, rotation));
	}



	
	private void placeGameObject(GameObject o, float distanceX, float distanceY, float f, bool isBossRoom = false, GenerateRoom room = null)
	{
		GameObject obj = Instantiate(o, new Vector3(distanceX, distanceY), Quaternion.Euler(0, 0, f));

		if (room != null)
		{
			obj.name = (room.GetRoomId()).ToString();
			obj.AddComponent<RoomReference>().room = room;
			
		}
		
		if (isBossRoom)
		{
			Renderer renderer = obj.GetComponent<Renderer>();
			if (renderer != null)
			{
				foreach (Material material in renderer.materials)
				{
					material.color = Color.yellow;
				}
			}
		}
	}

	
	private void paceWallOnMAp(int x, int y, int secondX, int secondY, float rotation)
	{
		Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, rotation), Vector3.one);
        
		gOptions.gridWall.SetTile(new Vector3Int(x , y), GetTileByNum(getNumOfTile("wBottom")));
		gOptions.gridWall.SetTransformMatrix(new Vector3Int(x , y), matrix);  
        
		gOptions.gridWall.SetTile(new Vector3Int(x + secondX, y- secondY), GetTileByNum(getNumOfTile("wTop")));
		gOptions.gridWall.SetTransformMatrix(new Vector3Int(x + secondX, y- secondY), matrix);  
	}
	
	private int getNumOfTile(string tileBlock)
	{
		//Es wird geschaut, welches Tile platziert werden muss
		int outputTile = 0;
		int count = 0;
		foreach (KeyValuePair<Tile, string> combi in gOptions.GetTileLinkedList())
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
	
	public Tile GetTileByNum(int num)
	{
		//Gibt den ersten wert von mapTile aus
		//(Ich habe keine bessere methode gefunden)
		int count = 0;
		foreach(KeyValuePair<Tile,string> pair in gOptions.GetTileLinkedList())
		{
			if(count == num)
			{
				return pair.Key;
			}

			count++;
		}
		return null;
	}
	

	private void GoThroughLayer()
	{
		int[,] layerArray = new GenerateLayer(gOptions).makeRooms();
		int layerLength = gOptions.layerArrayLength;

		//printArray(layerArray);
		
		for (int layerPosY = 0; layerPosY < layerLength; layerPosY++)
		{
			for (int layerPosX = 0; layerPosX < layerLength; layerPosX++)
			{
				if (layerArray[layerPosX, layerPosY] != 0)
				{
					int roomNumber = layerArray[layerPosX, layerPosY];
					//Debug.Log("roomtype: " + roomNumber);
					
					
					CreateRoom(roomNumber,layerPosX, layerPosY, layerArray);
				}
			}
		}
	}

	private void CreateRoom(int roomNumber, int layerPosX, int layerPosY, int[,] layerArray)
	{
		//log("CreateRoom ->" + roomNumber);
		int[] doorsPlace = new int[4];
		
		if (IsPossipleInArray(layerPosX, layerPosY - 1) && layerArray[layerPosX, layerPosY - 1] != 0) //dUp
		{
			doorsPlace[0] = layerArray[layerPosX, layerPosY - 1];
		}
		if (IsPossipleInArray(layerPosX, layerPosY + 1) && layerArray[layerPosX, layerPosY + 1] != 0) //dDown
		{
			doorsPlace[1] = layerArray[layerPosX, layerPosY + 1];
		}
		if (IsPossipleInArray(layerPosX - 1, layerPosY) && layerArray[layerPosX - 1, layerPosY] != 0) //dLeft
		{
			doorsPlace[2] = layerArray[layerPosX - 1, layerPosY];
		}
		if (IsPossipleInArray(layerPosX + 1, layerPosY) && layerArray[layerPosX + 1, layerPosY] != 0) //dRight
		{
			doorsPlace[3] = layerArray[layerPosX + 1, layerPosY];
		}
		

		
		rooms.AddLast(new GenerateRoom(gOptions,roomNumber,  layerPosX,  layerPosY, rooms.Count+1, doorsPlace));
	}

	private bool IsPossipleInArray(int pos1, int pos2)
	{
		int layerLength = gOptions.layerArrayLength;

		if (pos1 < 0 || pos1 > layerLength - 1 || pos2 < 0 || pos2 > layerLength - 1)
		{
			return false;
		}

		return true;
	}
	
	
	
	public void printArray(int[,] array)
	{ 
		//Der array wird in die Console geschrieben (du hättest auch einfach den namen der methode lesen können)
		for (int x = 0; x < array.GetLength(0); x++)
		{
			string msg = " ";
			for (int y = 0; y < array.GetLength(1); y++)
			{
				msg += " - " + array[y, x];
			}

			Debug.Log(x +": "+msg);
		}

	}

	private void log(object msg)
	{
		Debug.Log(msg);
	}
}

