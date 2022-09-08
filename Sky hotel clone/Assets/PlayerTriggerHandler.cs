using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour, IPlayerTrigger
{
    public void OnPlayerEnter(TriggerType Trigtype)
    {
        switch (Trigtype)
        {
            case TriggerType.dollar:
                HandleTriggerWithDollar();
                break;
            case TriggerType.Leftdoor:
                break;
            case TriggerType.Rightdoor:
                break;
        }
    }

    public void OnPlayerExit(TriggerType Trigtype)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerStay(TriggerType Trigtype)
    {
        throw new System.NotImplementedException();
    }


    private void HandleTriggerWithDollar()
    {
        throw new NotImplementedException();
    }
}
