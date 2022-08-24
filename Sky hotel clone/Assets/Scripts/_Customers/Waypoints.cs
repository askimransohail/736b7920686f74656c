using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
	public List<Transform> balloonSections;
	public List<GameObject> activeBalloonSections = new List<GameObject>();

	[SerializeField]private string searchTag;

	#region Waypoints Singleton Pattern
	public static Waypoints Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}
	#endregion

	private void Start()
	{
		UpdateWaypoints();
	}

	private void Update()
	{
		FindObjectwithTag(searchTag);
	}

	private void UpdateWaypoints()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			balloonSections.Add(transform.GetChild(i));
		}
	}

	public void FindObjectwithTag(string _tag)
	{
		activeBalloonSections.Clear();
		Transform parent = transform;
		GetChildObject(parent, _tag);
	}

	public void GetChildObject(Transform parent, string _tag) // This is for finding Balloon Sections which are tagged "Section" 
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.tag == _tag)
			{
				activeBalloonSections.Add(child.gameObject);
			}
			if (child.childCount > 0)
			{
				GetChildObject(child, _tag);
			}
		}
	}

}
