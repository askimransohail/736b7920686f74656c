using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave_State_Customer : BaseState
{
	protected SM_Customer smCustomer;

	public Leave_State_Customer(string name, SM_Customer stateMachine) : base(name, stateMachine)
	{
		smCustomer = stateMachine;
	}

	public override void Enter()
	{
		base.Enter();
		smCustomer.customer.animControllerCustomer.SetWalkingTrue();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();

		if (smCustomer.customer.customerBalloons.Count == 0)
			smCustomer.ChangeState(smCustomer.idleStateCustomer);

	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();

		smCustomer.customer.Agent.SetDestination(smCustomer.customer.leavePoint.position);
		if (!smCustomer.customer.Agent.pathPending && smCustomer.customer.Agent.remainingDistance < 1f)
		{
			smCustomer.customer.RemoveItem();
			smCustomer.customer.gameObject.SetActive(false);
		}

	}

}
