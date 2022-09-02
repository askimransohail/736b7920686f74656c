using System.Collections.Generic;
using Game.Script.CharacterBrain;
using Game.Script.Data;
using UnityEngine;

namespace Game.Script.CharacterBase
{
    public class CustomerManager : MonoBehaviour
    {
        public List<Transform> slots;
        public Queue<GameObject> customerQueue = new Queue<GameObject>();
        public Transform spawnPoint;
        public CharacterItem characterItem;
        public float generateTime;
        public int onTimeCustomer_count=2;
        private Transform _operationPoint;
        private float _timer;

        private void Update()
        {
            Tick();
        }

        #region Methots

        private void Tick()
        {
            _timer += Time.deltaTime;
            if (_timer > generateTime && customerQueue.Count < slots.Count && customerQueue.Count<= onTimeCustomer_count)
            {
                foreach (var item in customerQueue)
                {
                    item.GetComponent<CustomerBrain>().FindTarget();
                }
                var character = CharacterSpawner.Instance.CreateCharacter(characterItem, spawnPoint);
                customerQueue.Enqueue(character);
                _timer = 0;
            }
        }

        #endregion
    }
}