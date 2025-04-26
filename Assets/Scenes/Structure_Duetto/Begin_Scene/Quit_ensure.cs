using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_ensure : MonoBehaviour
{
    [SerializeField] private GameObject blurVolume;
    public GameObject QuitPanel;
    public GameObject DarkPanel;

    public void Exit()
    {
        Application.Quit();

    }
    public void StayInGame()
    {
        QuitPanel.SetActive(false);
        DarkPanel.SetActive(false);
        //blurVolume.SetActive(false);

    }
}
