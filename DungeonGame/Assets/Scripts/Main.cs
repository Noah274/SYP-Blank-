using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GeneratorOptions gOptions;
    private int roomNumber;
    
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

        GetRoomLocation();
        OpenRoom();
        
        CameraController camera = FindObjectOfType<CameraController>();
        if (camera != null)
        {
            camera.StartCamera(roomNumber);
        }
        else
        {
            Debug.LogError("Error: CameraController script not found!");
        }
    }

    private void GetRoomLocation()
    {
        RoomLogic logic = FindObjectOfType<RoomLogic>();
        if (logic != null)
        {
            logic.StartRoomLogic(gOptions, ref roomNumber);
        }
        else
        {
            Debug.LogError("Error: RoomLogic script not found!");
        }
    }

    private void OpenRoom()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in gameObjects)
        {
            if (obj.name == roomNumber.ToString())
            {
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = gOptions.doorOpen.GetComponent<SpriteRenderer>().sprite;
				
                spriteRenderer.color = Color.red;
                obj.GetComponent<RoomReference>().room.SetRoomDone();
            }
        }
    }
}
