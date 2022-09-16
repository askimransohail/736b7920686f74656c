using DG.Tweening;
using Game.Script.CharacterBrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashroomDoor : MonoBehaviour
{

    [SerializeField] private GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CustomerBrain brain))
        {
        print(other);
           // Door.GetComponent<DOTweenAnimation>().DORestart();
        }
    }
}
