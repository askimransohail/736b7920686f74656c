using _Adeel.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : Singleton<Tutorial>
{
    [SerializeField] private GameObject[] CamsView;
    [SerializeField] private UnityEvent AfterCam;

    int CamIndex;
    // Start is called before the first frame update
    void Start()
    {
        CamIndex = 0;
       // Invoke("CameraSwitch", 3f);
    }


    public void CameraSwitch()
    {
        print(PlayerPrefs.GetString("CamIndex" + CamIndex));
        //print(PlayerPrefs.GetInt("tutorial"));

        if (PlayerPrefs.GetString("CamIndex" + CamIndex) != "1" && PlayerPrefs.GetInt("tutorial")==0)
        {
            CamsView[CamIndex].SetActive(true);
            Invoke("TutorialCamOff", 3f);
        }
        PlayerPrefs.SetString("CamIndex" + CamIndex, "1");
       
    }

    void TutorialCamOff()
    {
        CamsView[CamIndex].SetActive(false);
      
        if (CamIndex < CamsView.Length)
            CamIndex++;
        if(CamIndex >= CamsView.Length)
            PlayerPrefs.SetInt("tutorial", 1);
    }

}
