using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GatherItem : MonoBehaviour
{
	[SerializeField] private float gatherCooldown = 120f;
	private float gatherTime = 0f;
	private bool gatheringReady = true;

	public void Gathering()
	{
		int playerBallonsCount = CharacterManager.Instance.playerBalloons.Count;

		if (playerBallonsCount < CharacterManager.Instance.playerBalloonsCapacity)
		{
			gatherTime++;

			if (gatherTime >= gatherCooldown)
			{
				gatheringReady = true;
				GatheringItem();
				ResetGatherTime();
			}
			else if (playerBallonsCount >= CharacterManager.Instance.playerBalloonsCapacity)
			{
				gatheringReady = false;
			}
		}
	}

	private void GatheringItem()
	{
		while (gatheringReady == true)
		{
			CharacterManager.Instance.AddItem();
			gatheringReady = false;
		}
	}

	public void ResetGatherTime()
	{
		gatherTime = 0;
	}

}
