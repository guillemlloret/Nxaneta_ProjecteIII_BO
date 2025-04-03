using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsBegin_Script : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
