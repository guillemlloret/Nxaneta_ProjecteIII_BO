using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Quit_PopUp : MonoBehaviour
{
    [SerializeField] private GameObject blurVolume;
    public GameObject Panel;
    public void OpenPanel()
    {
        if (Panel!=null)
        {
            Panel.SetActive(true);
            blurVolume.SetActive(true);

        }
    }
}
