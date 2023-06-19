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
        
        //Dungeon wird generiert
        GenerateGrid generate = FindObjectOfType<GenerateGrid>();
        if (generate != null)
        {
            generate.StartGenerationGrid(gOptions);
        }
        else
        {
            Debug.LogError("Error: GenerateGrid script not found!");
        }
        
        //Spieler wird zum Spawnraum gebracht
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.MovePlayer();
        }
        else
        {
            Debug.LogError("Error: Player script not found!");
        }

        JoinRoom();
    }

    public void JoinRoom()
    {
        //Der Raum, in dem sich der Spieler befindet (roomNumber die id des Raumes)
        RoomLogic logic = FindObjectOfType<RoomLogic>();
        if (logic != null)
        {
            logic.StartRoomLogic(gOptions, ref roomNumber);
        }
        else
        {
            Debug.LogError("Error: CameraController script not found!");
        }

        //Kamera wird auf den Raum gesetzt
        CameraController camera = FindObjectOfType<CameraController>();
        if (camera != null)
        {
            camera.StartCamera(roomNumber);
        }
        else
        {
            Debug.LogError("Error: CameraController script not found!");
        }
        
        //Gegner werden gespawnt
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
