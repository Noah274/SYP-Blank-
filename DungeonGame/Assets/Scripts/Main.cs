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
        RoomLogic logic = FindObjectOfType<RoomLogic>();
        if (logic != null)
        {
            logic.StartRoomLogic(gOptions, ref roomNumber);
        }
        else
        {
            Debug.LogError("Error: RoomLogic script not found!");
        }

        Camera.main.GetComponent<CameraController>().StartCamera(roomNumber);
    }
}
