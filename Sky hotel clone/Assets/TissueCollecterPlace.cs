using _Adeel.Helpers;
using _Adeel.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueCollecterPlace : Singleton<TissueCollecterPlace>
{
    // Start is called before the first frame update
    public static bool IsCollected = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            IsCollected = true;
           TissueManagment.Ins.TissueCollection();
           // print("enter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            IsCollected = false;
            //print("exit");
        }
    }
}
