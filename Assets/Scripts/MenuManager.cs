using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static  MenuManager Instance;

    [SerializeField] private GameObject _colorSelectPanel;
    

    void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        _colorSelectPanel.SetActive(state == GameState.SelectColor);
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
        SceneManager.LoadScene(2);
    }
    public void RetryScene2()
    {
        SceneManager.LoadScene(3);
    }
}
