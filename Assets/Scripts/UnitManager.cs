using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public GameObject TurnTutorial;
    public bool Level3 = false;
    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        if (Level3)
        {
            Debug.Log("start");
            GameManagerLvl3.Instance.UpdateGameState(GameState2.RedTurn);
            //TurnTutorial.SetActive(true);
        }
        else
        {
            Debug.Log("start");
            GameManager.Instance.UpdateGameState(GameState.RedTurn);
            TurnTutorial.SetActive(true);
        }
    }
}
