using System;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject leftPaddlePrefab;
    [SerializeField] private GameObject rightPaddlePrefab;
    [SerializeField] private GameObject ballPrefab;

    private NetworkVariable<int> player1Score = new NetworkVariable<int>(0);
    private NetworkVariable<int> player2Score = new NetworkVariable<int>(0);
    private NetworkVariable<bool> gameFinished = new NetworkVariable<bool>(false);
    private NetworkVariable<bool> gameStarted = new NetworkVariable<bool>(false);

    private int pointsToWin = 5;

    public void StartNewGame()
    {
        if (IsServer)
        {
            // // ensure both players exist
            // if (GameObject.FindGameObjectsWithTag("paddle").Length < 2)
            // {
            //     Debug.Log("Waiting for second player");
            //     return;
            // }

            //spawn ball (if it doesn't exist yet)
            if (GameObject.FindGameObjectWithTag("ball") == null)
            {
                SpawnBall();
            }

            // reset scores
            player1Score.Value = 0;
            player2Score.Value = 0;
            gameFinished.Value = false;
            gameStarted.Value = true;

            // 4. reset positions
            ResetPositions();

            Debug.Log("New game started!");
        }
    }
    void ResetPositions()
    {
        if (!IsServer) return;
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("paddle");
        GameObject ball = GameObject.FindGameObjectWithTag("ball");

        Debug.Log($"ResetPositions: paddles={paddles.Length}, ball={(ball != null)}");

        foreach (GameObject paddle in paddles)
        {
            if (paddle.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.position = new Vector2(rb.position.x, 0f);
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        resetBall(1);
    }

    // -1 = left, 1 = right
    void resetBall(int direction)
    {
        if (!IsServer) return;
        var ballObj = GameObject.FindGameObjectWithTag("ball");
        var ballMove = ballObj.GetComponent<BallMovement>();
        var rb = ballObj.GetComponent<Rigidbody2D>();

        rb.position = Vector2.zero;
        ballMove.Color = Color.white;

        float randomY = UnityEngine.Random.Range(-0.5f, 0.5f);

        ballMove.Direction = new Vector2(direction, randomY);
    }


    private void PauseBall()
    {
        var ballObj = GameObject.FindGameObjectWithTag("ball");
        if (ballObj == null) return;

        var ballMove = ballObj.GetComponent<BallMovement>();
        var rb = ballObj.GetComponent<Rigidbody2D>();

        ballMove.Direction = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }

    public int GetPlayer1Score()
    {
        return player1Score.Value;
    }

    public int GetPlayer2Score()
    {
        return player2Score.Value;
    }

    public int GetPointsToWin()
    {
        return pointsToWin;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        // // Spawn ball once match starts
        // SpawnBall();

        // Listen for players joining
        NetworkManager.OnClientConnectedCallback += OnClientConnected;
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        ball.GetComponent<NetworkObject>().Spawn();
    }

    private void OnClientConnected(ulong clientId)
    {
        if (!IsServer) return;

        SpawnPlayerPaddle(clientId);
    }




    private void SpawnPlayerPaddle(ulong clientId)
    {
        GameObject paddlePrefab =
            clientId == NetworkManager.ServerClientId
            ? leftPaddlePrefab
            : rightPaddlePrefab;

        Vector3 spawnPos =
            clientId == NetworkManager.ServerClientId
            ? new Vector3(-7, 0, 0)
            : new Vector3(7, 0, 0);

        GameObject paddle = Instantiate(paddlePrefab);
        paddle.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);


    }


    public void RecordScore(int scoreZoneNumber)
    {
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        switch (scoreZoneNumber)
        {
            case 1:
                IncreasePlayer1Score();
                resetBall(-1);
                break;
            case 2:
                IncreasePlayer2Score();
                resetBall(1);
                break;
            default:
                Debug.Log("Error with scoreZoneNumber");
                break;
        }
    }


    private void IncreasePlayer1Score()
    {
        if (IsServer && !gameFinished.Value)
        {
            player1Score.Value++;
            Debug.Log("Player 1 scores! New score: " + player1Score.Value);
            CheckWinCondition();
        }


    }

    private void IncreasePlayer2Score()
    {
        if (IsServer && !gameFinished.Value)
        {
            player2Score.Value++;
            Debug.Log("Player 2 scores! New score: " + player2Score.Value);
            CheckWinCondition();
        }

    }

    private void CheckWinCondition()
    {
        if (player1Score.Value >= pointsToWin)
        {
            gameFinished.Value = true;
            gameStarted.Value = false;
            PauseBall();
            Debug.Log("Game complete! Player 1 Wins!");
        }

        if (player2Score.Value >= pointsToWin)
        {
            gameFinished.Value = true;
            gameStarted.Value = false;
            PauseBall();
            Debug.Log("Game complete! Player 1 Wins!");
        }
    }

    public bool isGameFinished()
    {
        return gameFinished.Value;
    }

    public bool IsGameStarted()
    {
        return gameStarted.Value;
    }
}



