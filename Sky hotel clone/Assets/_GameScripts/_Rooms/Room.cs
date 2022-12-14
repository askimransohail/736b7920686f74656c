using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomState
{
    available,
    dirty,
    booked,
    Occupied
}

public class Room : MonoBehaviour
{
    //[SerializeField]private States[] RoomState;
    public RoomState roomState;
    public Transform customerTarget;
    public Transform sleepTarget;
    public GameObject[] cleanThings, dirtyThings, occupy;
    [SerializeField] private GameObject Dustbin;
    public static event Action<GameObject> OnRoomDirty;
    public  int count=0;

    bool isClean = true;
    // Start is called before the first frame update
    void Start()
    {
        updateRoomStates(RoomState.available);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    public void updateRoomStates(RoomState _roomState)
    {
        roomState = _roomState;
        switch (_roomState)
        {
            case RoomState.available:
                isClean = true; 
                roomCondition();
                break;
            case RoomState.dirty:
                isClean = false;
                HandleRoomOccupy();
                roomCondition();
                OnRoomDirty?.Invoke(gameObject);
                break;
            case RoomState.booked:
                break;
            case RoomState.Occupied:
                HandleRoomOccupy();
                break;
        }
    }

    private void HandleRoomOccupy()
    {
        for (int i = 0; i < occupy.Length; i++)
        {
            if(roomState==RoomState.Occupied)
            occupy[i].SetActive(true);
            else
                occupy[i].SetActive(false);


        }
    }

    void roomCondition()
    {
        foreach (var things in cleanThings)
        {
            things.SetActive(isClean);
            Dustbin.SetActive(isClean);
        }
        foreach (var things in dirtyThings)
        {
            things.SetActive(!isClean);
            Dustbin.SetActive(!isClean);

        }
    }

 public void checkRoomCondition()
    {
        count++;
        if (count == cleanThings.Length)
        {
            print(count);
            updateRoomStates(RoomState.available);
            Tutorial.Ins.CameraSwitch();

            count = 0;
        }
        
    }



}
