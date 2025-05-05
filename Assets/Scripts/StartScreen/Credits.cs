using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Credits : MonoBehaviour
{
    public GameObject CreditsScreen;
    public GameObject VideoPlayerObject;
    public AudioSource BGMusic_LevelMenu;

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = VideoPlayerObject.GetComponent<VideoPlayer>();

        
        videoPlayer.started += OnVideoStarted;

        
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void OpenCredits()
    {
        CreditsScreen.SetActive(true);
        VideoPlayerObject.SetActive(true);
        videoPlayer.Play(); 

        if (BGMusic_LevelMenu != null && BGMusic_LevelMenu.isPlaying)
        {
            BGMusic_LevelMenu.Stop(); 
        }
    }

    public void SkipCredits()
    {
        videoPlayer.Stop(); 
        CloseCredits();     
    }

    private void OnVideoStarted(VideoPlayer vp)
    {
        if (BGMusic_LevelMenu != null && BGMusic_LevelMenu.isPlaying)
        {
            BGMusic_LevelMenu.Stop();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        CloseCredits();
    }

    private void CloseCredits()
    {
        CreditsScreen.SetActive(false);
        VideoPlayerObject.SetActive(false);

        if (BGMusic_LevelMenu != null)
        {
            BGMusic_LevelMenu.Play();
        }
    }
}
