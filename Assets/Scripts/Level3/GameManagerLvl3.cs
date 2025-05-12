using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.VisionOS;
using UnityEngine;
using UnityEngine.UI;

public enum GameState2
{
    SelectColor,
    RedTurn,
    BlueTurn,
    Decide,
    Victory,
    Lose
}
public class GameManagerLvl3 : MonoBehaviour
{
    [SerializeField] PointsHUD movements;

    public static GameManagerLvl3 Instance;
    public GameState2 State;

    public GameObject textVictoria;
    public GameObject textDerrota;

    public bool RedWon;
    public bool BlueWon;

    public Image image;
    public Sprite red_Sprite;
    public Sprite blue_Sprite;

    public bool PurpleUp = false;
    public bool LightGreenUp = false;
    public bool DarkGreenOpen = false;

    public bool Level3 = false;


    public static event Action<GameState2> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {

    }
    void Start()
    {
        UpdateGameState(GameState2.SelectColor);

    }
    public void UpdateGameState(GameState2 newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState2.SelectColor:
                HandleSelectColor();
                break;
            case GameState2.RedTurn:
                HandleRedTurn();
                break;
            case GameState2.BlueTurn:
                HandleBlueTurn();
                break;
            case GameState2.Decide:
                HandleDecide();
                break;
            case GameState2.Victory:
                HandleVictory();
                break;
            case GameState2.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
    private void HandleSelectColor()
    {

    }

    private void HandleDecide()
    {

    }
    private void HandleRedTurn()
    {
        Debug.Log("Change state to red");
        PlayerControllerRed2.Instance.ChooseTileRed();
        image.GetComponent<Image>().sprite = red_Sprite;


    }
    private void HandleBlueTurn()
    {
        Debug.Log("Change state to blue");
        PlayerControllerBlue2.Instance.ChooseTileBlue();
        image.GetComponent<Image>().sprite = blue_Sprite;


    }
    private void HandleVictory()
    {

        textVictoria.SetActive(true);
    }
    private void HandleLose()
    {

        textDerrota.SetActive(true);
    }

}


