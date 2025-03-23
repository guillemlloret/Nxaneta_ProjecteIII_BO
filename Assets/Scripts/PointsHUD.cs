using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PointsHUD : MonoBehaviour
{
    [SerializeField] TMP_Text movementsText;

    int movements = 20;

    private void Awake()
    {
        UpdateHUD();
    }
    public int Movements
    {
        get
        {
            return movements;
        }

        set
        {
            movements = value;
            UpdateHUD();
        }
    }
   
    private void UpdateHUD ()
    {
        movementsText.text = movements.ToString();
    }
}
