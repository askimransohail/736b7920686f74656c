using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacking : MonoBehaviour
{
    private bool isInTrigArea;

    [SerializeField] private string tagToCompare = "TissueRack";
    [SerializeField] private float trigStayDelay, stackDelay;
    [SerializeField] private GameObject stackItemPrefab;
    [SerializeField] private Transform stackPoint, stackContainer;
    [SerializeField] private Transform pickPoint;
    private float YAxis=0;
    Transform item;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            isInTrigArea = true;
            //item = Instantiate(stackItemPrefab,pickPoint);

            //item.transform.parent = stackContainer;
            DOVirtual.DelayedCall(trigStayDelay, () => CO_Stacking(stackDelay,other.gameObject));
        }
    }

    private void CO_Stacking(float delay,GameObject other)
    {
        Debug.Log("CO_Stacking");
        while (isInTrigArea)
        {
            Debug.Log("isInTrigArea");
           // yield return new WaitForSeconds(delay);
            item = other.gameObject.transform;

            item.transform.parent = null;
            item.transform.SetParent(stackContainer);
            //var targetPoint = stackPoint + height
           //item.transform.SetParent(stackContainer);
            item.transform.DOLocalJump(stackPoint.position, 0.5f, 1, 1);
            //YAxis += .5f;

        }
        //yield return null;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            isInTrigArea = false;
        }
    }
}
