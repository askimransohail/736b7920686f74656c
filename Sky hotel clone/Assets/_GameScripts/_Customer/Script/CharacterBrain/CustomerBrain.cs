using System.Linq;
using Game.Script.CharacterBase;
using Game.Script.Zone;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

namespace Game.Script.CharacterBrain
{
    public enum CustomerState
    {
        Bring,
        Ready,
        Wait,
        Collect,
        Sleep,
        washroom,
        Destroy,
        Nothing
    }

    public class CustomerBrain : CharacterBrainBase
    {
        public Transform target;
        public CustomerState customerState;
        public Transform destroyZone;
        private CustomerManager _customerManager;
        [SerializeField] private ReceptionZone receptionZone;
        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");
        [SerializeField] private SpriteRenderer progressBar;
        [SerializeField] private GameObject unlockProgressBar;
        private Room RoomInstance;
        private Transform ToiletInstance;

        private bool IsroomAloted = false;
        bool AnimComplete = true;
        #region Unity Lifecycle

        private void Awake()
        {
            var parent = transform.parent;
            _customerManager = parent.parent.GetComponentInChildren<CustomerManager>();
          // print(parent.parent.GetComponentInChildren<CustomerManager>());
            customerState = CustomerState.Bring;
            destroyZone = GameObject.FindGameObjectWithTag("DestroyZone").transform;
            receptionZone = transform.root.GetComponentInChildren<ReceptionZone>();
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
                      //  print(this.gameObject);
                        Movement();
                        customerState = CustomerState.Ready;
                        break;

                    case CustomerState.Ready when IsroomAloted:
                        unlockProgressBar.SetActive(true);
                        UnlockProgress();
                   // IsroomAloted = false;
                        break;

                    case CustomerState.Collect:
                        unlockProgressBar.SetActive(false);
                    print(target);

                    Movement();
                        break;
                    case CustomerState.Sleep:
                   
                      SleepTime();
                    
                        break;
                case CustomerState.washroom:
                    RefreshTime();
                    break;
                    case CustomerState.Destroy:
                        Movement();
                        break;
                }
            
        }

        void SleepTime()
        {
            transform.DOMove(RoomInstance.sleepTarget.position, 1f).OnComplete(() =>
            {

                if (AnimComplete)
                {
                    Animator.SetBool("sleep", true);
                    AnimComplete = false;
                    Invoke("SleepToAwake", 4f);
                }

            });
        }
        void SleepToAwake()
        {
            customerState = CustomerState.Nothing;
            Animator.SetBool("sleep", false);

            transform.DOMove(RoomInstance.customerTarget.position, 1f).OnComplete(() =>
            {
                RoomInstance.GetComponent<Room>().updateRoomStates(RoomState.dirty);
                FindWashroom();
            });

        }

        void RefreshTime()
        {
            Movement();
            if (!IsDestinationReach())
            {
                Animator.SetBool("pee", true);
                customerState = CustomerState.Nothing;
                GetComponent<Rigidbody>().isKinematic = true;
                Invoke("BackToHome", 3f);
            }
        }
        void BackToHome()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Animator.SetBool("pee", false);
            ToiletInstance.GetComponent<Toilets>().updateToiletStates(ToiletState.OutOfService);
            customerState = CustomerState.Destroy;
            target = destroyZone;

        }


        public void FindTarget()
        {
            target = _customerManager.slots[_customerManager.customerQueue.ToList().IndexOf(transform.gameObject)];
          
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
           if(customerState != CustomerState.Sleep)
            Animator.SetFloat("Velocity", NavMeshAgent.velocity.magnitude);

        }

        public override void Movement()
        {
            if (customerState != CustomerState.Sleep)
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
            {
                StartCoroutine(Dollar.ins.MakeMoney());
                customerState = CustomerState.Collect;
                _customerManager.customerQueue.Dequeue();
            }

        }
        public void roomAloted(Transform room)
        {
            RoomInstance = room.GetComponent<Room>();
            target = RoomInstance.customerTarget;
            IsroomAloted = true;
        }

        public void FindWashroom()
        {
            ToiletInstance = WashroomManagement.Ins.IsToiletAvailable();
            target = ToiletInstance.GetComponent<Toilets>().PeePoint;
            customerState = CustomerState.washroom;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag=="Sleep")
            {
                other.GetComponent<BoxCollider>().enabled = false;
                customerState = CustomerState.Sleep;
                target = null;
                RoomInstance.updateRoomStates(RoomState.Occupied);
              
            }

        }
    }
}