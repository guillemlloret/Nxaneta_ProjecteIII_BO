using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.VisionOS;
using UnityEngine;
using UnityEngine.UI;


public enum GameState
{
    SelectColor,
    RedTurn,
    BlueTurn,
    Decide,
    Victory,
    Lose
}


public class GameManager : MonoBehaviour
{
    [SerializeField] PointsHUD movements;

    public static GameManager Instance;
    public GameState State;

    public GameObject textVictoria;
    public GameObject textDerrota;

    public bool RedWon;
    public bool BlueWon;

    public Image image;
    public Sprite red_Sprite;
    public Sprite blue_Sprite;


    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;    
    }
   void Update()
    {
       
    }
    void Start()
    {
        UpdateGameState(GameState.SelectColor);

    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SelectColor:
                HandleSelectColor();
                break;
            case GameState.RedTurn:
                HandleRedTurn();
                break;
            case GameState.BlueTurn:
                HandleBlueTurn();
                break;
            case GameState.Decide:
                HandleDecide();
                break;  
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
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
        PlayerControllerRed.Instance.ChooseTileRed();
        image.GetComponent<Image>().sprite = red_Sprite;

    }
    private  void HandleBlueTurn()
    {
        Debug.Log("Change state to blue");
        PlayerControllerBlue.Instance.ChooseTileBlue();
        image.GetComponent<Image>().sprite= blue_Sprite;


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



