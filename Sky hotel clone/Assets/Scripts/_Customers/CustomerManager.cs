using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
	[SerializeField] private List<Transform> customers;
	//private int activeSections;

	#region CustomerManager Singleton Pattern
	public static CustomerManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}
	#endregion

	private void Start()
	{
		GetCustomerList();
	}

	private void Update()
	{
		Invoke(nameof(SetCustomerAsActive), 1);
	}

	private void GetCustomerList()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			customers.Add(transform.GetChild(i));
		}
	}

	private void SetCustomerAsActive()
	{
		for (int i = 0; i < Waypoints.Instance.activeBalloonSections.Count; i++)
		{
			customers[i].gameObject.SetActive(true);
		}
	}

}
