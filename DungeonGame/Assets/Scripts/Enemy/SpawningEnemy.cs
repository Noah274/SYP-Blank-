using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemy : MonoBehaviour
{
    private System.Random _random = new System.Random();
    public void StartSpawningEnemies(GeneratorOptions gOptions,int roomNumber)
    {
        Debug.Log("Spawning Enemys" + roomNumber);
        int level = gOptions.layerLevel;
        int room = roomNumber;

        int enemyCount = _random.Next(2, level + 4) * 2;
        
        GameObject targetRoom = GameObject.Find("RoomCenterPoint_" + roomNumber.ToString());
        Vector3 pos = targetRoom.transform.position;
        
        GameObject block = new GameObject("block");
        block.transform.position = pos;
    }
}
