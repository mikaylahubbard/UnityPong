using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject leftPaddlePrefab;
    [SerializeField] private GameObject rightPaddlePrefab;
    [SerializeField] private GameObject ballPrefab;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        // Spawn ball once match starts
        SpawnBall();

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
}

