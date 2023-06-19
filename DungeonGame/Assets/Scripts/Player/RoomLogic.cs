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
        
        string roomCenterPointPrefix = "RoomCenterPoint_" + numberString;
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in gameObjects)
        {
            if (obj.name.StartsWith(roomCenterPointPrefix))
            {
                this.objRoom = obj;
            }
        }
    }

    public void OpenRoom(int number)//int number
    {
        //Debug.Log("number: " + number);
        
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in gameObjects)
        {
            if (obj.name == number.ToString())
            {
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = gOptions.doorOpen.GetComponent<SpriteRenderer>().sprite;
            }
        }
        
        string roomCenterPointPrefix = "RoomCenterPoint_" + number.ToString();
         gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in gameObjects)
        {
            if (obj.name.StartsWith(roomCenterPointPrefix))
            {
                GenerateRoom roomObj = obj.GetComponent<RoomReference>().room;
                roomObj.SetRoomDone();
            }
        }
    }

    public void TeleportToNextRoom(Quaternion rotation)
    {
        int roomID = 0;
        GenerateRoom roomObj = objRoom.GetComponent<RoomReference>().room;

        if (roomObj != null && roomObj.GetRoomDone())
        {
            //Debug.Log("Start Teleport");
            int rotationZ = (int) rotation.eulerAngles.z;
            GameObject teleportPoint = null;
            if (rotationZ == 0)
            {
                roomID = roomObj.GetTelIdUp();
                string roomCenterPointName = "RoomCenterPoint_" + roomID;
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            else if (rotationZ == 90)
            {
                roomID = roomObj.GetTelIdLeft();
                string roomCenterPointName = "RoomCenterPoint_" + roomID;
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            else if (rotationZ == 180)
            {
                roomID = roomObj.GetTelIdDown();
                string roomCenterPointName = "RoomCenterPoint_" + roomID;
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            else if (rotationZ == 270)
            {
                roomID = roomObj.GetTelIdRight();
                string roomCenterPointName = "RoomCenterPoint_" + roomID;
                teleportPoint = GameObject.Find(roomCenterPointName);
            }
            
            
            if (teleportPoint != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                
                player.transform.position = teleportPoint.transform.position;
                player.transform.rotation = teleportPoint.transform.rotation;
                
                /*
                CameraController camera = FindObjectOfType<CameraController>();
                if (camera != null)
                {
                    camera.StartCamera(roomID);
                }
                else
                {
                    Debug.LogError("Error: CameraController script not found!");
                }*/
                
                //Raum Logik neu ausführen
                Main main = FindObjectOfType<Main>();
                if (main != null)
                {
                    main.JoinRoom();
                }
                else
                {
                    Debug.LogError("Error: Main script not found!");
                }
            }
        }
    }




}
