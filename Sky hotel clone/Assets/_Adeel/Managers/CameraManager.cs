
using Cinemachine;
using _Adeel.Helpers;

namespace _Adeel.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        public CinemachineVirtualCamera[] Cams;

        public void SwitchCameras(int camIndex)
        {
            foreach (var cam in Cams)
            {
                cam.Priority = 10;
            }
            Cams[camIndex].Priority = 20;
        }
    }
}


