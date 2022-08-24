using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
	public NavMeshAgent Agent;
	public Transform Target;

	[Header("Item Prefab")]
	[SerializeField] public GameObject itemPrefab = null;

	public List<GameObject> customerBalloons;
	public int customerBalloonsCapacity = 3;

	public Transform cashierPaymentPoint;
	public Transform cashierWaitingPoint;
	public Transform leavePoint;

	public AIAnimator animControllerCustomer;

	public virtual void Awake()
	{
		Target = null;
	}

	public virtual void Start()
	{
		Agent = GetComponent<NavMeshAgent>();
		animControllerCustomer = GetComponentInParent<AIAnimator>();
	}

	public virtual void AddItem()
	{
		customerBalloons.Add(itemPrefab);
		int ballonCount = customerBalloons.Count;

		foreach (GameObject balloon in customerBalloons)
		{
			transform.GetChild(1).GetChild(ballonCount - 1).gameObject.SetActive(true);
		}
	}

	public virtual void RemoveItem()
	{
		int ballonCount = customerBalloons.Count;

		foreach (GameObject balloon in customerBalloons)
		{
			transform.GetChild(1).GetChild(ballonCount - 1).gameObject.SetActive(false);
		}
		customerBalloons.Remove(itemPrefab);
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Section"))
		{
			int balloonsInSection = other.gameObject.GetComponent<DropItem>().sectionsBalloons.Count;

			if (balloonsInSection > 0)
			{
				TakingItem();
			}
		}
	}

	public void TakingItem()
	{
		int customerBallonsCount = customerBalloons.Count;

		if (customerBallonsCount < customerBalloonsCapacity)
		{
			AddItem();
		}
	}

}
