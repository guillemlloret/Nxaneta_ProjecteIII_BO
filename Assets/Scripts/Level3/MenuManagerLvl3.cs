using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManagerLvl3 : MonoBehaviour
{
    public static MenuManagerLvl3 Instance;

    [SerializeField] private GameObject _colorSelectPanel;

    public GameObject Restart3;
    void Awake()
    {
        Restart3.SetActive(false);
        Instance = this;
        GameManagerLvl3.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManagerLvl3.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
    private void GameManagerOnOnGameStateChanged(GameState2 state)
    {
        _colorSelectPanel.SetActive(state == GameState2.SelectColor);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(3);
    }
    public void ExitLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void RetryScene()
    {
        SceneManager.LoadScene(6);
    }
    public void RetryScene2()
    {
        SceneManager.LoadScene(3);
    }

    public void ShowPauseLvl3()
    {
        Restart3.SetActive(true);
    }
    public void HidePauseLvl3()
    {
        Restart3.SetActive(false);
    }
}
