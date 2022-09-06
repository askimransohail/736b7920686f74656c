using _Adeel.Player;
using Game.Script.CharacterBase;
using Game.Script.CharacterBrain;
using UnityEngine;

namespace Game.Script.Zone
{
    public class PickupDollar : MonoBehaviour
    {


        private void Awake()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag=="dollar")
            {
Destroy(other.gameObject); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
       
        }
    }
}