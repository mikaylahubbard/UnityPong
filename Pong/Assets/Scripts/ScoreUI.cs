using Unity.Netcode;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI winMessageText;
    public Button startButton;
    private string player1Score;
    private string player2Score;

    private GameManager gameManager;




    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        winMessageText.gameObject.SetActive(false);
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        gameManager.StartNewGame();
        startButton.gameObject.SetActive(false);
        winMessageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameManager.isGameFinished())
        {
            winMessageText.gameObject.SetActive(true);

            startButton.gameObject.SetActive(true);

            if (gameManager.GetPlayer1Score() >= gameManager.GetPointsToWin())
            {
                winMessageText.text = "Player 1 Wins!";
            }
            else
            {
                winMessageText.text = "Player 2 Wins!";
            }
        }
        if (!gameManager.IsGameStarted())
        {
            startButton.gameObject.SetActive(true);
        }
        else
        {
            winMessageText.gameObject.SetActive(false);
            startButton.gameObject.SetActive(false);
        }

        // Read NetworkVariable values and update UI
        player1Score = gameManager.GetPlayer1Score().ToString();
        player2Score = gameManager.GetPlayer2Score().ToString();
        Score.text = player1Score + " - " + player2Score;
    }
}