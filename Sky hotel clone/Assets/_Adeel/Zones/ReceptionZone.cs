using _Adeel.Player;
using Game.Script.CharacterBase;
using Game.Script.CharacterBrain;
using UnityEngine;

namespace Game.Script.Zone
{
    public class ReceptionZone : MonoBehaviour
    {

      //  public bool roomAvailable { get; set; }
        public bool isPlayerInReceptionZone { get; set; }

        public bool isReceptionistInReceptionZone;

        public bool isCustomerInReceptionZone { get; set; }

        public static ReceptionZone instance;
        private void Awake()
        {
            instance = this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                isPlayerInReceptionZone = true;


            }
            if ( other.gameObject.tag == "Reception")
            {
                isReceptionistInReceptionZone = true;


            }
            if (other.TryGetComponent(out CustomerBrain _))
            {
                isCustomerInReceptionZone = true;
              
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _) )
            {
                isPlayerInReceptionZone = false;
            }
            if (other.TryGetComponent(out CustomerBrain _))
            {
                isCustomerInReceptionZone = false;
            }
        }
    }
}