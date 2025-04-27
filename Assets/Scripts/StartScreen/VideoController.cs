using UnityEngine;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    public GameObject CreditsScreen;
    public GameObject VideoPlayer;
    //public Button skipButton;

    
    public void PlayVideo()
    {
        CreditsScreen.SetActive(true);
        VideoPlayer.SetActive(true);
    }

    public void SkipVideo()
    {
        EndVideo();
    }

    void EndVideo()
    {
        CreditsScreen.SetActive(false);
        VideoPlayer.SetActive(false);
        //skipButton.gameObject.SetActive(false);
    }
}
