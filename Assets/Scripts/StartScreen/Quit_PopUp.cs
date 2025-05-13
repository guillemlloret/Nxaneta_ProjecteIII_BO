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
    public GameObject SettingsPanel;
    public GameObject DarkPanel;
    public void OpenQuitPanel()
    {
        if (QuitPanel != null)
        {
            QuitPanel.SetActive(true);
            DarkPanel.SetActive(true);
        }
    }
    public void OpenSettingsPanel()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(true);
            DarkPanel.SetActive(true);
        }
    }
}
