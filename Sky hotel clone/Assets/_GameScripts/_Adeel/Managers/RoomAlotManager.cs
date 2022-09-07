using Game.Script.CharacterBase;
using Game.Script.CharacterBrain;
using Game.Script.Zone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAlotManager : MonoBehaviour
{
    private CustomerManager _customerManager;

    private void Awake()
    {
        _customerManager = FindObjectOfType<CustomerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ReceptionZone.instance.isPlayerInReceptionZone)
        {
            Transform room=RoomManagement.Instance.IsroomAvailable();
           // print(room);
            if (room != null && ReceptionZone.instance.isCustomerInReceptionZone)
            {
                print(_customerManager);
                room.GetComponent<Room>().updateRoomStates(RoomState.booked);
                _customerManager.customerQueue.Peek().GetComponent<CustomerBrain>().roomAloted(room);
                ReceptionZone.instance.isCustomerInReceptionZone = false;
            }

        }
        
    }
}
