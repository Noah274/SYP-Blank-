using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject targetRoom;
    private Vector3 offset;
    private int spawn = 1;
    
    public void StartCamera(int roomNumber)
    {
        FindTargetRoom(roomNumber); 
    }
    
    void LateUpdate()
    {
        if (targetRoom != null )
        {
            //Debug.Log("Camera pos: " + transform.position);
            //Debug.Log("target: "+ targetRoom.transform.position);
            transform.position = new Vector3(targetRoom.transform.position.x, targetRoom.transform.position.y, -10);
            //Debug.Log("new Pos: " + transform.position);

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
}