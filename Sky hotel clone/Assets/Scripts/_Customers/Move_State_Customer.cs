using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_State_Customer : BaseState
{
	protected SM_Customer smCustomer;
	private Transform target;

	[Header("Waypoint Variables")]
	private Transform targetWp;
	private int wayPointIndex = 0;

	public Move_State_Customer(string name, SM_Customer stateMachine) : base(name, stateMachine)
	{
		smCustomer = stateMachine;
	}

	public override void Enter()
	{
		base.Enter();
		target = smCustomer.customer.Target;
		smCustomer.customer.animControllerCustomer.SetWalkingTrue();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		if (smCustomer.customer.customerBalloons.Count < 1) // if customer has not balloon go to section to take balloon
		{
			if (target == null)
			{
				target = PickWaypoint();

				if (target != null && target.GetChild(0).CompareTag("Section")) // if there is available section go to section
					smCustomer.customer.Agent.SetDestination(target.position);

				else
					smCustomer.ChangeState(smCustomer.idleStateCustomer); // if there is no available section to take balloon go Idle State and keep waiting
			}
		}

		else // When customer got balloon go to cashier
		{
			if (CharacterManager.Instance.playerIsCashier == true)
			{
				smCustomer.customer.Agent.speed = 3f;
				smCustomer.customer.Agent.SetDestination(smCustomer.customer.cashierPaymentPoint.position);
				if (!smCustomer.customer.Agent.pathPending && smCustomer.customer.Agent.remainingDistance < 1f)
				{
					smCustomer.ChangeState(smCustomer.leaveStateCustomer);
					Debug.Log("REACHED  PAYMENT POINT ");
				}
			}
			else
			{
				smCustomer.customer.Agent.SetDestination(smCustomer.customer.cashierWaitingPoint.position);
				if (!smCustomer.customer.Agent.pathPending && smCustomer.customer.Agent.remainingDistance < 1f)
				{
					smCustomer.customer.Agent.speed = 0f;
					smCustomer.customer.animControllerCustomer.SetWalkingFalse();
					Debug.Log("REACHED WAITING POINT ");
				}
			}
		}
	}

	private Transform PickWaypoint()
	{
		wayPointIndex = Random.Range(0, 3);
		targetWp = Waypoints.Instance.balloonSections[wayPointIndex];
		return targetWp;
	}

}

