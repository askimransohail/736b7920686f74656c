using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
	Animator animator;

	private void Start()
	{
		animator = GetComponentInChildren<Animator>();
	}

	public void SetWalkingTrue()
	{
		animator.SetBool("isWalking", true);
	}

	public void SetWalkingFalse()
	{
		animator.SetBool("isWalking", false);
	}

}
