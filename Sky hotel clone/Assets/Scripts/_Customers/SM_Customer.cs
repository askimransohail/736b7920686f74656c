using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Customer : StateMachine
{
    public Move_State_Customer moveStateCustomer;
    public Idle_State_Customer idleStateCustomer;
    public Leave_State_Customer leaveStateCustomer;

    public Customer customer => GetComponent<Customer>();

    public virtual void Awake()
    {
        //customer = GetComponent<Customer>();
        moveStateCustomer = new Move_State_Customer("CustomerMove",this);
        idleStateCustomer = new Idle_State_Customer("CustomerIdle", this);
        leaveStateCustomer = new Leave_State_Customer("CustomerLeave", this);
    }

    protected override BaseState GetInitialState()
    {
        return idleStateCustomer;
    }

}
