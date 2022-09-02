using System.Linq;
using Game.Script.CharacterBase;
using Game.Script.Zone;
using UnityEngine;

namespace Game.Script.CharacterBrain
{
    public enum CustomerState
    {
        Bring,
        Ready,
        Wait,
        Collect,
        Destroy
    }

    public class CustomerBrain : CharacterBrainBase
    {
        public Transform target;
        public CustomerState customerState;
        public Transform destroyZone;
        private CustomerManager _customerManager;
        [SerializeField] private WaitZone waitZone;
        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");
        [SerializeField] private SpriteRenderer progressBar;
        [SerializeField] private GameObject unlockProgressBar;



        #region Unity Lifecycle

        private void Awake()
        {
            var parent = transform.parent;
            _customerManager = parent.parent.GetComponentInChildren<CustomerManager>();
            print(parent.parent.GetComponentInChildren<CustomerManager>());
            customerState = CustomerState.Bring;
            destroyZone = GameObject.FindGameObjectWithTag("DestroyZone").transform;
            waitZone = transform.root.GetComponentInChildren<WaitZone>();
        }

        protected new void Start()
        {
            base.Start();
        }

        #endregion


        private bool IsDestinationReach()
        {

           
            float distanceToTarget = Vector3.Distance(transform.position, NavMeshAgent.destination);

            if (distanceToTarget < NavMeshAgent.stoppingDistance)
            {
                return false;
            }

            return true;
        }

        void CheckCustomerSituation()
        {
            switch (customerState)
            {
                case CustomerState.Bring when !IsDestinationReach():
                    customerState = CustomerState.Wait;
                    break;

                case CustomerState.Wait when _customerManager.customerQueue.ToList().IndexOf(transform.gameObject) == 0:
                    target = _customerManager.slots[_customerManager.customerQueue.ToList().IndexOf(transform.gameObject)];
                    Movement();
                    Invoke("ChangrState",2f);
                    break;

                case CustomerState.Ready when waitZone.ReachWaitZone:
                    unlockProgressBar.SetActive(true);
                    UnlockProgress();
                    break;

                case CustomerState.Collect:
                    unlockProgressBar.SetActive(false);

                    target = destroyZone;
                    print(waitZone.ReachWaitZone);

                    Movement();
                    customerState = CustomerState.Destroy;
                    _customerManager.customerQueue.Dequeue();
                    break;
                case CustomerState.Destroy:
                    break;
            }
        }

        void ChangrState()
        {
            if(!IsDestinationReach())
            customerState = CustomerState.Ready;

        }
        public void FindTarget()
        {
            target = _customerManager.slots[_customerManager.customerQueue.ToList().IndexOf(transform.gameObject)];
            print(target);
            //if (_customerManager.customerQueue.ToList().IndexOf(transform.gameObject) == 0 &&
            //    customerState == CustomerState.Ready)
            //{
            //    //customerState = CustomerState.Collect;
            //    UnlockProgress();

            //}
            Movement();
        }

        protected override void InitProperties()
        {
            base.InitProperties();
            NavMeshAgent.speed = characterItem.movementSpeed;
            NavMeshAgent.stoppingDistance = characterItem.stoppingDistance;
            NavMeshAgent.acceleration = characterItem.acceleration;
            NavMeshAgent.angularSpeed = characterItem.angularSpeed;

            FindTarget();
        }

        public override void Logic()
        {
            
            IsDestinationReach();
            CheckCustomerSituation();
            Animator.SetFloat("Velocity", NavMeshAgent.velocity.magnitude);
        }

        public override void Movement()
        {
            NavMeshAgent.SetDestination(target.position);
        }

        float progress = 0f;
        private void UnlockProgress()
        {
            if (progress < 1f)
            {
                progress += 2f * Time.deltaTime;
                progressBar.material.SetFloat(Arc1, 360f - progress * 360f);
            } 
            else
                customerState = CustomerState.Collect;

        }
    }
}