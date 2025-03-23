using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        Debug.Log("start");
        GameManager.Instance.UpdateGameState(GameState.RedTurn);
    }
}
