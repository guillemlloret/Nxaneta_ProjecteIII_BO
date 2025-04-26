using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject CreditsScreen;
    public GameObject VideoPlayer;

    public void OpenCredits()
    {
        CreditsScreen.SetActive(true);
        VideoPlayer.SetActive(true);

    }
}
