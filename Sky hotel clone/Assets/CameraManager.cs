using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using _Adeel.Helpers;

public class CameraManager : Singleton<CameraManager>
{
    public  CinemachineVirtualCamera[] Cams;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public  void SwitchCameras(int camIndex)
    {
        foreach(var cam in Cams)
        {
            cam.Priority = 10;
        }
        Cams[camIndex].Priority = 20;
    }
}
