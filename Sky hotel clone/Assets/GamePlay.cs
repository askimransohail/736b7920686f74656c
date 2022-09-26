using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Adeel.Helpers;
public class GamePlay : Singleton<GamePlay>
{
    [SerializeField] private GameObject[] UnlockingPrefabs;
    int UnlockingPrefabIndex;
    float timeDelay =3f;
    // Start is called before the first frame update
    void Start()
    {
        //UnlockingPrefabIndex=0;
       // InvokeRepeating("EnableUnlockingPrefab", timeDelay, 20f);
    }

public void EnableUnlockingPrefab()
    {
        if(UnlockingPrefabIndex<UnlockingPrefabs.Length)
        UnlockingPrefabs[UnlockingPrefabIndex].SetActive(true);
        UnlockingPrefabIndex++;
    }
}
