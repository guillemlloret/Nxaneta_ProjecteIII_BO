using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public Button nextButton;
    public GameObject movementsAvailable;
    public GameObject movementsHighlight;

    private string[] tutorialMessages = {

        "Guide each piece to its destination.",
        "Move by using the left mouse button.",
        "Available tiles for movement will be highlighted.",
        "You have a limited number of moves to complete each level.",
        "Avoid obstacles and reach your goal!",
        "Good luck!",

    };

    private int currentMessageIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    public GameObject StartGame;

    public float typingSpeed = 0.05f; // Velocitat de la màquina d'escriure

    void Start()
    {
        StartGame.SetActive(false);
        tutorialPanel.SetActive(true);
        ShowCurrentMessage();
        nextButton.onClick.AddListener(NextMessage);
        Time.timeScale = 0f; // Pausa el joc mentre fem el tutorial
    }

    void ShowCurrentMessage()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(tutorialMessages[currentMessageIndex]));
    }

    // Aquesta funció la pots cridar quan vulguis per mostrar un missatge immediat
    public void ShowSpecificMessage(string message)
    {
        tutorialPanel.SetActive(true);
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(message));
        Time.timeScale = 0f; // Opcional: Pausar el joc si vols durant aquest missatge
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        tutorialText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    void NextMessage()
    {
        if (isTyping)
        {
            // Si estem escrivint, saltem a finalitzar el text de cop
            StopCoroutine(typingCoroutine);
            tutorialText.text = tutorialMessages[currentMessageIndex];
            isTyping = false;
        }
        else
        {
            currentMessageIndex++;

            if (currentMessageIndex == 3)
            {
                movementsAvailable.SetActive(true);
                movementsHighlight.SetActive(true); 
            }
            if (currentMessageIndex >= tutorialMessages.Length)
            {
                tutorialPanel.SetActive(false);
                StartGame.SetActive(true);
                Time.timeScale = 1f; // Reprèn el joc
            }
            else
            {
                ShowCurrentMessage();
            }
        }
    }
}
