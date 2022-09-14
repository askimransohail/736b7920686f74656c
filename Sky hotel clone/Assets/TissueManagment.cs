using _Adeel.Helpers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Adeel;
using _Adeel.Player;
using System.Linq;

public class TissueManagment : Singleton<TissueManagment>
{
    [SerializeField] private Transform TissueObject;
    [SerializeField] private Transform TissueRackPlace;
    [SerializeField] private Transform TissuePlace;
    [SerializeField] private List<Transform> Tissue = new List<Transform>();
    private float YAxis, delay;
    int count;
    int TissueCount = 0;

    // Start is called before the first frame update

    private void Start()
    {
       // Tissue.Add(TissuePlace);

    }


    void Update()
    {
        //if (Tissue.Count > 1)
        //{
        //    for (int i = 1; i < Tissue.Count; i++)
        //    {
        //        var firstPaper = Tissue.ElementAt(i - 1);
        //        var secondPaper = Tissue.ElementAt(i);

        //        secondPaper.position = new Vector3(Mathf.Lerp(secondPaper.position.x, firstPaper.position.x, Time.deltaTime * 15f),
        //        Mathf.Lerp(secondPaper.position.y, firstPaper.position.y + 0.3f, Time.deltaTime * 15f), firstPaper.position.z);
        //    }
        //}

        //if (Physics.Raycast(transform.position, transform.forward, out var hit, 3f))
        //{
        //    Debug.DrawRay(transform.position, transform.forward * 1f, Color.green);
        //if (TissueCollecterPlace.IsCollected && Tissue.Count < 6)
        ////if (hit.collider.CompareTag("TissueRack") && Tissue.Count < 6)

        //{
        //    var TissueInstace = Instantiate(TissueObject, TissueRackPlace);
        //        TissueInstace.rotation = Quaternion.Euler(TissueInstace.rotation.x, Random.Range(0f, 180f), TissueInstace.rotation.z);
        //        Tissue.Add(TissueInstace);
        //        //paper.parent = null;

        //        //if (hit.collider.transform.parent.GetComponent<Printer>().YAxis > 0f)
        //        //    hit.collider.transform.parent.GetComponent<Printer>().YAxis -= 0.17f;


        //    }
        //}
    }
    // Update is called once per frame
    public void TissueCollection()
    {
       
        if (Tissue.Count < 3 && TissueCollecterPlace.IsCollected)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;

            var TissueInstace = Instantiate(TissueObject, TissueRackPlace);
            TissueInstace.DOJump(new Vector3(TissuePlace.position.x, TissuePlace.position.y + YAxis, TissuePlace.position.z), 2f, 1, 0.2f)
                           .SetDelay(delay).SetEase(Ease.Flash).OnComplete(() =>
                           {
                               TissueInstace.rotation = TissuePlace.rotation;
                               TissueInstace.parent = TissuePlace;
                               Tissue.Add(TissueInstace);
                               YAxis += .5f;
                               delay += 0.4f;
                               //GetComponent<Animator>().enabled = true;
                               TissueCollection();
                           });
        }

        else 
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<PlayerMovement>().enabled = true;
        }


    }
}
