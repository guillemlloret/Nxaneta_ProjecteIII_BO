using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; 

public class ButtonTextHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public TextMeshProUGUI tmpText; 
    [Header("Hex Colors (e.g. #222222)")]
    public string normalColorHex = "#B0B0B0"; // gris
    public string hoverColorHex = "#222222"; // negro

    private Color normalColor;
    private Color hoverColor;
    private FontStyle originalStyle;

    private void Start()
    {
        // Convertir los colores hex a Color
        ColorUtility.TryParseHtmlString(normalColorHex, out normalColor);
        ColorUtility.TryParseHtmlString(hoverColorHex, out hoverColor);

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tmpText != null)
        {
            tmpText.color = hoverColor;
            tmpText.fontStyle = FontStyles.Bold;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tmpText != null)
        {
            tmpText.color = normalColor;
            tmpText.fontStyle = FontStyles.Normal;
        }
    }
}
