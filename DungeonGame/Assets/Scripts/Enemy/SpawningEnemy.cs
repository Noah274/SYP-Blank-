using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawningEnemy : MonoBehaviour
{
    private System.Random _random = new System.Random();
    private GeneratorOptions gOptions;
    private int roomNumber;
    private bool endetSpawning = false;
    public void StartSpawningEnemies(GeneratorOptions gOptions,int roomNumber)
    {
        this.gOptions = gOptions;
        this.roomNumber = roomNumber;
        
        int level = gOptions.layerLevel;

        int enemyCount = _random.Next(2, level + 4) * 2;
        //Debug.Log("EnemyCount: " + enemyCount);

        StartSpawning(enemyCount);
    }

    private void Update()
    {
        if (endetSpawning)
        {
            CheckIfRoomDone();   
        }
    }


    private void CheckIfRoomDone()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    
        if (enemies.Length == 0)
        {
            endetSpawning = false;
            
            RoomLogic logic = FindObjectOfType<RoomLogic>();
            if (logic != null)
            {
                logic.OpenRoom(roomNumber);
            }
            else
            {
                Debug.LogError("Error: Main script not found!");
            }
        }
    }

    private void StartSpawning(int enemyCount)
    {
        StartCoroutine(SpawnEnemies(enemyCount));
    }

    private IEnumerator SpawnEnemies(int enemyCount)
    {
        GameObject targetRoom = GameObject.Find("RoomCenterPoint_" + roomNumber.ToString());
        Vector3 centerPos = targetRoom.transform.position;
    
        int enemyPlaced = 0;
        int circleRadius = gOptions.spawnRange;

        for (int i = 0; i < enemyCount+1; i++)
        {
            if (enemyPlaced < enemyCount)
            {
                enemyPlaced++;
                    
                float randomOffsetX = _random.Next(-circleRadius, circleRadius);
                float randomOffsetY = _random.Next(-circleRadius, circleRadius);
                
                Vector3 enemySpawnPos = centerPos + new Vector3(randomOffsetX, randomOffsetY, 0);
                GameObject enemy = Instantiate(gOptions.enemy[_random.Next(0, gOptions.enemy.Length)], enemySpawnPos, Quaternion.identity);
                enemy.tag = "Enemy";
                
                enemy.GetComponent<EnemyAI>().hitPoints = 1;
                
                float randomDelay = Random.Range(0, gOptions.spawnDelay);
                yield return new WaitForSeconds(randomDelay);
            }
            else
            {
                endetSpawning = true;
            }
        }
    }


}
