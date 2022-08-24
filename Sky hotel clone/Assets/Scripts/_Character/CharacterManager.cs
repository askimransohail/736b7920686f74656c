using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
	[Header("Item Prefab")]
	[SerializeField] public GameObject balloonsPrefab = null;

	public List<GameObject> playerBalloons;
	public int playerBalloonsCapacity = 5;

	public bool playerIsCashier;

	#region Singleton Pattern
	public static CharacterManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}
	#endregion

	public void AddItem()
	{
		playerBalloons.Add(balloonsPrefab);
		int ballonCount = playerBalloons.Count;

		foreach (GameObject balloon in playerBalloons)
		{
			transform.GetChild(1).GetChild(ballonCount - 1).gameObject.SetActive(true);
		}
	}

	public void RemoveItem()
	{
		int ballonCount = playerBalloons.Count;

		foreach (GameObject balloon in playerBalloons)
		{
			transform.GetChild(1).GetChild(ballonCount-1).gameObject.SetActive(false);
		}
		playerBalloons.Remove(balloonsPrefab);
	}

	public void TakeMoney()
	{
		playerIsCashier = true;
	}

	public void UpdateCashierStatus()
	{
		playerIsCashier = false;
	}

}
