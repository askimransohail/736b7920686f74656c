using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour
{
	Animator customerAnimator;

	private void Start()
	{
		customerAnimator = GetComponentInChildren<Animator>();
	}

	public void SetWalkingTrue()
	{
		customerAnimator.SetBool("isWalking", true);
	}

	public void SetWalkingFalse()
	{
		customerAnimator.SetBool("isWalking", false);
	}

}
