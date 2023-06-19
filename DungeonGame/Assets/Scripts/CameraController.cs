using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetRoom;
    private Vector3 offset;
    
    public void StartCamera(int roomNumber)
    {
        FindTargetRoom(roomNumber);
        CalculateOffset();
    }
    
    void LateUpdate()
    {
        if (targetRoom != null)
        {
            transform.position = -offset;
        }
    }

    void FindTargetRoom(int roomNumber)
    {
        targetRoom = GameObject.Find("RoomCenterPoint_" + roomNumber.ToString());
        
        if (targetRoom == null)
        {
            Debug.LogWarning("RoomCenterPoint_" + roomNumber.ToString() + " not found!");
        }
        
    }

    void CalculateOffset()
    {
        if (targetRoom != null)
        {
            offset = transform.position - targetRoom.transform.position;
        }
    }
}