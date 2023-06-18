using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetRoom;
    private Vector3 offset;
    
    public void StartCamera()
    {
        Debug.Log("test");
        FindTargetRoom();
        CalculateOffset();
        Debug.Log("test 2");
    }
    void LateUpdate()
    {
        if (targetRoom != null)
        {
            // Set the camera position to the center of the target room
            transform.position = targetRoom.transform.position + offset;
        }
    }

    void FindTargetRoom()
    {
        int roomNumber = 1;
        targetRoom = GameObject.Find("RoomcenterPoint_" + roomNumber.ToString());
        Debug.Log(targetRoom.ToString());
    }

    void CalculateOffset()
    {
        if (targetRoom != null)
        {
            offset = transform.position - targetRoom.transform.position;
        }

        Debug.Log("calculate offset");
    }
}