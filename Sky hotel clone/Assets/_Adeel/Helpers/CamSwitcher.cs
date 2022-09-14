using _Adeel.Managers;
using _Adeel.Player;
using UnityEngine;


namespace _Adeel.Helpers
{
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
}