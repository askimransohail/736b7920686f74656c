using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	public List<GameObject> sectionsBalloons;
	[SerializeField] int sectionCapacity = 3;
	private bool outOfStock = true;

	private void Update()
	{
		if (sectionsBalloons.Count <= 0)
			outOfStock = true;
		else
			outOfStock = false;

		StockChange();
	}

	public void Dropping()
	{
		if (sectionsBalloons.Count < sectionCapacity && CharacterManager.Instance.playerBalloons.Count > 0)
		{
			for (int i = 0; i < CharacterManager.Instance.playerBalloons.Count; i++)
			{
				sectionsBalloons.Add(CharacterManager.Instance.balloonsPrefab);
				CharacterManager.Instance.RemoveItem();
				transform.GetChild(0).GetChild(sectionsBalloons.Count - 1).gameObject.SetActive(true);
			}
		}
	}

	public void CustomerTaking()
	{
		foreach (GameObject balloon in sectionsBalloons)
		{
			transform.GetChild(0).GetChild(sectionsBalloons.Count - 1).gameObject.SetActive(false);
		}
		sectionsBalloons.Remove(CharacterManager.Instance.balloonsPrefab);
	}

	void StockChange()
	{
		if (outOfStock == false)
			gameObject.tag = ("Section");
		else
			gameObject.tag = ("Untagged");
	}

}
