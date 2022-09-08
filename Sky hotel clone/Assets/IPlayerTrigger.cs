using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerTrigger 
{
    public void OnPlayerEnter(TriggerType Trigtype);
    public void OnPlayerExit(TriggerType Trigtype); 
    public void OnPlayerStay(TriggerType Trigtype);


}
