using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Dollar : MonoBehaviour
{
    [SerializeField] private Transform DollarPlace;
    [SerializeField] private GameObject dollar;
    private float YAxis;
    //private IEnumerator makeMoneyIE;
    public static Dollar ins;
    int counter = 0;
    int DollarPlaceIndex = 0;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
      //StartCoroutine( MakeMoney());
    }

    public  IEnumerator MakeMoney()
    {
       
        
        yield return new WaitForSecondsRealtime(2);

        //Transform DollarPosition = DollarPlace.GetChild(DollarPlaceIndex);
        //GameObject NewDollar = Instantiate(dollar);
        ////NewDollar.transform.position = DollarPosition.transform.position;
        ////NewDollar.transform.rotation = DollarPosition.transform.rotation;
        GameObject NewDollar = Instantiate(dollar, new Vector3(DollarPlace.GetChild(DollarPlaceIndex).position.x,
                   YAxis, DollarPlace.GetChild(DollarPlaceIndex).position.z),
               DollarPlace.GetChild(DollarPlaceIndex).rotation);
        NewDollar.transform.DOScale(new Vector3(15f, 15f, 15f), 1f).SetEase(Ease.OutElastic);

            if (DollarPlaceIndex < DollarPlace.childCount - 1)
            {
                DollarPlaceIndex++;
            }
            else
            {
                DollarPlaceIndex = 0;
                YAxis += .18f;
            }
   
    }

}
