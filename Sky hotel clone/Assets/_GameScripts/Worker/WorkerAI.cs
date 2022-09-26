using _Adeel.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : Singleton<WorkerAI>
{
    public WorkerState Worker_State;
    [SerializeField] private Animator workerAnim;
    [SerializeField] private List<GameObject> WorkerAIList = new List<GameObject>();
    protected NavMeshAgent NavMeshAgent => gameObject.GetComponent<NavMeshAgent>();

    RoomManagement Rooms;
    [SerializeField] private Transform Initialtarget;

    Transform target;
    private void OnEnable()
    {
        Room.OnRoomDirty += AddWorkingList;
    }

    private void OnDisable()
    {
        Room.OnRoomDirty -= AddWorkingList;

    }
    void Awake()
    {
        Rooms = RoomManagement.Instance;
        workerAnim = workerAnim.GetComponent<Animator>();
        UpdateWorkerStates(WorkerState.Free);
    }
    void Start()
    {
        foreach (GameObject room in Rooms.roomList)
        {
            if (room.GetComponent<Room>().roomState == RoomState.dirty)
                AddWorkingList(room);
        }
        
    }


    public void UpdateWorkerStates(WorkerState workerState)
    {
        Worker_State = workerState;
        switch (workerState)
        {
            case WorkerState.Free:
                target = Initialtarget;
                break;
            case WorkerState.Busy:
                break;
        }
    }


    private void Update()
    {
           // IsDestinationReach();
            NavMeshAgent.SetDestination(target.position);
            workerAnim.SetFloat("Velocity", NavMeshAgent.velocity.magnitude);
        if (Worker_State == WorkerState.Busy && WorkerAIList.Count != 0)
        {
            FindTarget();
            if (WorkerAIList[0].GetComponent<Room>().roomState != RoomState.dirty)
            {
                changeRoom();
            }
        }


    }
    void FindTarget()
    {
        if (WorkerAIList.Count != 0)
        {
            target = WorkerAIList[0].GetComponent<Room>().dirtyThings[WorkerAIList[0].GetComponent<Room>().count].transform;
          //  print(target);

        }


    }

    private bool IsDestinationReach()
    {

        float distanceToTarget = Vector3.Distance(transform.position, NavMeshAgent.destination);
        if (distanceToTarget < NavMeshAgent.stoppingDistance)
        {
            return false;
        }

        return true;
    }

    void changeRoom()
    {
        WorkerAIList.RemoveAt(0);
        if (WorkerAIList.Count == 0)
        {
            UpdateWorkerStates(WorkerState.Free);
        }
    }

    private void AddWorkingList(GameObject obj)
    {
        WorkerAIList.Add(obj);
        UpdateWorkerStates(WorkerState.Busy);
    }


}

public enum WorkerState
{
    Free,
    Busy
}
