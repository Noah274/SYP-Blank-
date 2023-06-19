using System;
using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GeneratorOptions gOptions;
    private int roomNumber = 0;
    
    IEnumerator Start()
    {
        yield return null;

        gOptions = gameObject.GetComponent<GeneratorOptions>();
        
        GenerateGrid generate = FindObjectOfType<GenerateGrid>();
        if (generate != null)
        {
            generate.StartGenerationGrid(gOptions);
        }
        else
        {
            Debug.LogError("Error: GenerateGrid script not found!");
        }
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.MovePlayer();
        }
        else
        {
            Debug.LogError("Error: Player script not found!");
        }

        RoomLogic logic = FindObjectOfType<RoomLogic>();
        if (logic != null)
        {
            logic.StartRoomLogic(gOptions, ref roomNumber);
        }
        else
        {
            Debug.LogError("Error: CameraController script not found!");
        }

        
        CameraController camera = FindObjectOfType<CameraController>();
        if (camera != null)
        {
            camera.StartCamera(roomNumber);
        }
        else
        {
            Debug.LogError("Error: CameraController script not found!");
        }
        
        SpawningEnemy enemy = FindObjectOfType<SpawningEnemy>();
        if (enemy != null)
        {
            enemy.StartSpawningEnemies(gOptions, roomNumber);
        }
        else
        {
            Debug.LogError("Error: SpawningEnemy script not found!");
        }
    }

    
}
