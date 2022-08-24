using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
        Debug.Log(this.name + " is created");
    }

    public virtual void Enter()
    {
        Debug.Log("....entering " + name + " state");
    }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit()
    {
        Debug.Log("....exiting " + name + " state");
    }
}