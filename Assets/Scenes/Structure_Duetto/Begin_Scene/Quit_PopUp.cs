using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Quit_PopUp : MonoBehaviour
{
    public GameObject Panel;
    public void OpenPanel()
    {
        if (Panel!=null)
        {
            Panel.SetActive(true);
        }
    }
}
