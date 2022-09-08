using _Adeel.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{
    [SerializeField] private int camIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            CameraManager.Ins.SwitchCameras(camIndex);
        }
    }
}
