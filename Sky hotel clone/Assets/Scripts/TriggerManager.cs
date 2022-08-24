using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeathenEngineering.Events;

public class TriggerManager : MonoBehaviour
{
	[Header("ENTER EVENT")]
	public UnityColliderEvent TriggerEnter;
	[SerializeField] private string enterTag = null;

	[Header("STAY EVENT")]
	public UnityColliderEvent TriggerStay;
	[SerializeField] private string stayTag = null;

	[Header("EXIT EVENT")]
	public UnityColliderEvent TriggerExit;
	[SerializeField] private string exitTag = null;

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.CompareTag(enterTag)) TriggerEnter.Invoke(other);
	}

	private void OnTriggerStay(Collider other)
	{
		
		if (other.gameObject.CompareTag(stayTag)) TriggerStay.Invoke(other);
	}

	private void OnTriggerExit(Collider other)
	{
		
		if (other.gameObject.CompareTag(exitTag)) TriggerExit.Invoke(other);
	}
}
