using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_ensure : MonoBehaviour
{
    [SerializeField] private GameObject blurVolume;
    public GameObject Panel;
    public void Exit()
    {
        Application.Quit();

    }
    public void StayInGame()
    {
        Panel.SetActive(false);
        blurVolume.SetActive(false);

    }
}
