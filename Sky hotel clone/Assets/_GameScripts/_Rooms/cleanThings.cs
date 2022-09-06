using _Adeel.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cleanThings : MonoBehaviour
{

    private static readonly int Arc1 = Shader.PropertyToID("_Arc1");
    [SerializeField] private SpriteRenderer progressBar;
    //[SerializeField] private GameObject unlockProgressBar;
    [SerializeField] private UnityEvent OnClean;

    float progress = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement _))
        {
            if (progress < 1f)
            {
                progress += 2f * Time.deltaTime;
                progressBar.material.SetFloat(Arc1, 360f - progress * 360f);
            }
            else
                OnClean?.Invoke();
        }


    }
}
