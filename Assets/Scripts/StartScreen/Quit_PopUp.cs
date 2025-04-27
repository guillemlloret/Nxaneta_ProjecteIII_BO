using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Quit_PopUp : MonoBehaviour
{
    [SerializeField] private GameObject blurVolume;
    public GameObject QuitPanel;
    public GameObject DarkPanel;
    public void OpenPanel()
    {
        if (QuitPanel!=null)
        {
            QuitPanel.SetActive(true);
            DarkPanel.SetActive(true);

            //blurVolume.SetActive(true);

        }
    }
}
