using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State_Customer : BaseState
{
	protected SM_Customer smCustomer;
	public GameObject[] activeSections;

	public Idle_State_Customer(string name, SM_Customer stateMachine) : base(name, stateMachine)
	{
		smCustomer = stateMachine;
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		smCustomer.customer.animControllerCustomer.SetWalkingFalse();
		activeSections = GameObject.FindGameObjectsWithTag("Section");

		if (activeSections.Length <= 0)
			Debug.Log("Active Sections = "+ activeSections.Length);

		else 
			smCustomer.ChangeState(smCustomer.moveStateCustomer);

	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();

	}
}
