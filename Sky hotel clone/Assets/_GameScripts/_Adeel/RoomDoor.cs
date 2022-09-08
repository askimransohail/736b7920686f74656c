using _Adeel.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{

    public DoorType Door_type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            CameraManager.Ins.SwitchCameras((int)Door_type);
        
        }
    }
}