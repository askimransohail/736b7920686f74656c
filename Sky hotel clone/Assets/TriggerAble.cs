using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAble : MonoBehaviour
{

    public TriggerType triggerType;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IPlayerTrigger playerTrigger))
        {
            playerTrigger.OnPlayerEnter(triggerType);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IPlayerTrigger playerTrigger))
        {
            playerTrigger.OnPlayerStay(triggerType);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IPlayerTrigger playerTrigger))
        {
            playerTrigger.OnPlayerExit(triggerType);
        }
    }
}
