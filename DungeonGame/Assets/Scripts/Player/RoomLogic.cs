using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
    private GeneratorOptions gOptions;
    private GameObject objRoom;
    public void StartRoomLogic(GeneratorOptions options, ref int roomNumber)
    {
        this.gOptions = options;
        this.objRoom = null;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] roomCenterPoints = GameObject.FindGameObjectsWithTag("RoomCenterPoint");

        string nearestRoomCenterPointName = "";
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject roomCenterPoint in roomCenterPoints)
        {
            float distance = Vector3.Distance(player.transform.position, roomCenterPoint.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestRoomCenterPointName = roomCenterPoint.name;
            }
        }

        //Debug.Log("Nearest RoomCenterPoint to player: " + nearestRoomCenterPointName);
        
        string[] splitString = nearestRoomCenterPointName.Split('_');
        string numberString = splitString[1];
        roomNumber = Int32.Parse(numberString);
	        
        //Debug.Log(numberString + "----------");
        
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in gameObjects)
        {
            if (obj.name == numberString)
            {
                this.objRoom = obj;
            }
        }
    }

    public void OpenRoom()//int number
    {
        //Debug.Log("number: " + number);
        
        SpriteRenderer spriteRenderer = objRoom.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gOptions.doorOpen.GetComponent<SpriteRenderer>().sprite;
        
        GenerateRoom roomObj = objRoom.GetComponent<RoomReference>().room;
        roomObj.SetRoomDone();
    }

    public void TeleportToNextRoom(Quaternion rotation)
    {
        GenerateRoom roomObj = objRoom.GetComponent<RoomReference>().room;

        if (roomObj != null && roomObj.GetRoomDone())
        {
            Debug.Log("Start Teleport");
            GameObject teleportPoint = null;
            if (rotation.y == 0)
            {
                string roomCenterPointName = "RoomCenterPoint_" + roomObj.GetTelIdUp();
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            else if (rotation.y == 90)
            {
                string roomCenterPointName = "RoomCenterPoint_" + roomObj.GetTelIdLeft();
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            else if (rotation.y == 180)
            {
                string roomCenterPointName = "RoomCenterPoint_" + roomObj.GetTelIdDown();
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            else if (rotation.y == 270)
            {
                string roomCenterPointName = "RoomCenterPoint_" + roomObj.GetTelIdRight();
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            
            
            if (teleportPoint != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                
                player.transform.position = teleportPoint.transform.position;
                player.transform.rotation = teleportPoint.transform.rotation;

            }
            
            //Raum Logik neu ausführen
            /*
            Main main = FindObjectOfType<Main>();
            if (main != null)
            {
                main.JoinRoom();
            }
            else
            {
                Debug.LogError("Error: CameraController script not found!");
            }*/
        }
    }




}
