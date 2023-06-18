using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        GenerateGrid generate = FindObjectOfType<GenerateGrid>();
        if (generate != null)
        {
            generate.StartGenerationGrid();
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
            logic.StartRoomLogic();
        }
        else
        {
            Debug.LogError("Error: RoomLogic script not found!");
        }
        CameraController camera = FindObjectOfType<CameraController>();
        if (camera != null)
        {
            camera.StartCamera();
        }
        else
        {
            Debug.LogError("Error: CameraController script not found!");
        }
    }

}
