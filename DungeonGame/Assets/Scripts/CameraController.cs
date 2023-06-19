using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetRoom;
    private Vector3 offset;
    private int spawn = 1;
    
    public void StartCamera(int roomNumber)
    {
        FindTargetRoom(roomNumber);
        CalculateOffset();
    }
    
    void LateUpdate()
    {
        if (targetRoom != null )
        {
            Debug.Log("Camera pos: " + transform.position);
            Debug.Log("target: "+ targetRoom.transform.position);
            Debug.Log("Offset: "+ offset); ;
            Debug.Log("New Pos:" + targetRoom.transform.position + offset);
            transform.position = targetRoom.transform.position + offset;
            
        }
    }


    void FindTargetRoom(int roomNumber)
    {
        targetRoom = GameObject.Find("RoomCenterPoint_" + roomNumber.ToString());
        Debug.Log(targetRoom.transform.position);
        if (targetRoom == null)
        {
            Debug.LogWarning("RoomCenterPoint_" + roomNumber.ToString() + " not found!");
        }
        
    }

    void CalculateOffset()
    {
        if (targetRoom != null)
        {
            offset = targetRoom.transform.position - transform.position;
            Debug.Log(offset);
        }
    }
}