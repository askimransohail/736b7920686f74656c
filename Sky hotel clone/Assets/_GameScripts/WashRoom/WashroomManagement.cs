using System;
using System.Collections.Generic;
using UnityEngine;
using _Adeel.Helpers;

public class WashroomManagement : Singleton<WashroomManagement>
{
    public List<Toilets> Toilets = new List<Toilets>();

    private void Awake()
    {
        //WashRoomList = new List<WashRoom>();
    }

    public void AddToilet(Toilets toilet)
    {
        Toilets.Add(toilet);
        toilet.toiletState = ToiletState.Available;
    }

    public Transform IsToiletAvailable()
    {
        foreach (var child in Toilets)
        {
            if (child.GetComponent<Toilets>().toiletState == ToiletState.Available)
            {
                child.GetComponent<Toilets>().updateToiletStates(ToiletState.Busy);
                return child.transform;
            }

        }

        return null;


    }


}



