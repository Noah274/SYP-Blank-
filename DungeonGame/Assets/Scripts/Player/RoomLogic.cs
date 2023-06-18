using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
	private GeneratorOptions gOptions;
    public void StartRoomLogic(GeneratorOptions options, ref int roomNumber)
    {
	    this.gOptions = options;
	    
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
				SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
				spriteRenderer.sprite = gOptions.doorOpen.GetComponent<SpriteRenderer>().sprite;
				
				spriteRenderer.color = Color.red;
				obj.GetComponent<RoomReference>().room.SetRoomDone();
			}
		}
    }

    
}
