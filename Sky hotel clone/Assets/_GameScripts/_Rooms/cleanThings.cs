using _Adeel.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cleanThings : MonoBehaviour
{

    private static readonly int Arc1 = Shader.PropertyToID("_Arc1");
    [SerializeField] private SpriteRenderer progressBar;
   // [SerializeField] private GameObject UnlockProgressBarobj;
    [SerializeField] private GameObject roomObject;
    [SerializeField] private UnityEvent OnClean;
    
    float progress = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //UnlockProgressBarobj.SetActive(true);   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement _) || other.TryGetComponent(out WorkerAI _))
        {
            if (progress < 1f)
            {
                progress += .5f * Time.deltaTime;
                progressBar.material.SetFloat(Arc1, 360f - progress * 360f);
            }
            else
            {
                OnClean?.Invoke();
                progressBar.material.SetFloat(Arc1, 360f);
                progress = 0f;
                roomObject.GetComponent<Room>().checkRoomCondition();
            }
        }


    }
}
