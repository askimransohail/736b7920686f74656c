using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomState
{
    available,
    dirty,
    booked
}

public class Room : MonoBehaviour
{
    //[SerializeField]private States[] RoomState;
    public RoomState roomState;
    public Transform customerTarget;
    [SerializeField] private GameObject[] cleanThings;
    [SerializeField] private GameObject[] dirtyThings;
 

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
                roomCondition();
                break;
            case RoomState.booked:

                break;
            

        }


    }

    void roomCondition()
    {
        foreach (var things in cleanThings)
        {
            things.SetActive(isClean);
        }
        foreach (var things in dirtyThings)
        {
            things.SetActive(!isClean);
        }

    }

 

}
