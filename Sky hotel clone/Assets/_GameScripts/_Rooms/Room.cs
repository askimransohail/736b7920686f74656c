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
    [SerializeField] private RoomState roomState;
    [SerializeField] private GameObject[] cleanThings;
    [SerializeField] private GameObject[] dirtyThings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    public void updateRoomStates()
    {
        switch (roomState)
        {
            case RoomState.available:

                break;
            case RoomState.dirty:

                break;
            case RoomState.booked:

                break;
            

        }


    }



}

//[Serializable]
//public struct States
//{
//    public RoomState room_State;
//    public GameObject[] cleanThings;
//    public GameObject[] dirtyThings;

//}
