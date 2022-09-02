using _Adeel.Player;
using Game.Script.CharacterBase;
using Game.Script.CharacterBrain;
using UnityEngine;

namespace Game.Script.Zone
{
    public class WaitZone : MonoBehaviour
    {
        private float _timer;
        private CustomerManager _customerManager;

        public bool ReachWaitZone { get; set; }


        private void Awake()
        {
            _customerManager = FindObjectOfType<CustomerManager>();
         
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                ReachWaitZone = true;
//                foreach (var s in _customerManager.customerQueue)
//                {
//                    Debug.Log(s);
//;                }
//                Debug.Log("First" + _customerManager.customerQueue.Peek());
//                _customerManager.customerQueue.Dequeue();
//                foreach (var s in _customerManager.customerQueue)
//                {
//                    Debug.Log("After Dequeue"+s);
                    
//                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _) )
            {
                ReachWaitZone = false;
                print(ReachWaitZone);

            }
        }
    }
}