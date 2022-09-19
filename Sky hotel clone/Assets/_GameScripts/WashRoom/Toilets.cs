using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilets : MonoBehaviour
{
    public ToiletState toiletState;
    WashroomManagement washroomManagement;
    public Transform PeePoint;
    [SerializeField] private GameObject Things;
    //bool isClean = true;

    // Start is called before the first frame update
    void Start()
    {
        washroomManagement = WashroomManagement.Ins;
        washroomManagement.AddToilet(this);
    }
    public void updateToiletStates(ToiletState _ToiletState)
    {
        toiletState = _ToiletState;
        switch (_ToiletState)
        {
            case ToiletState.Available:
                ToiletCondition(false);
                break;
            case ToiletState.Busy:
                break;
            case ToiletState.OutOfService:
                ToiletCondition(true);
                break;
        }

    }
    void ToiletCondition(bool isClean)
    {

        Things.SetActive(isClean);
        
    }
}

    public enum ToiletState
{
    Available,
    Busy,
    OutOfService
}
