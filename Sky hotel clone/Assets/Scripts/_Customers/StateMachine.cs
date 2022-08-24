using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	public BaseState CurrentState { get; private set; } //this is the current state the state machine is in.

	void Start()
	{
		CurrentState = GetInitialState();
		if (CurrentState != null)
			CurrentState.Enter();
	}

	// this is for LOGIC
	private void Update()
	{
		if (CurrentState != null)
			CurrentState.UpdateLogic();

	}

	// this is for PHYSICS and MATH
	private void FixedUpdate()
	{
		if (CurrentState != null)
			CurrentState.UpdatePhysics();

	}

	protected virtual BaseState GetInitialState()
	{
		return null;
	}

	public void ChangeState(BaseState newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}

}
