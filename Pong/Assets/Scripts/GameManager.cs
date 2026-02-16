using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public GameObject leftPaddlePrefab;
    public GameObject rightPaddlePrefab;
    public GameObject ballPrefab;
    private int playerCount = 0;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        SpawnBall();
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        ball.GetComponent<NetworkObject>().Spawn();
    }



    private void OnClientConnected(ulong clientId)
    {
        if (!IsServer) return;

        GameObject prefabToSpawn =
            playerCount == 0 ? leftPaddlePrefab : rightPaddlePrefab;

        GameObject paddle = Instantiate(prefabToSpawn);

        paddle.GetComponent<NetworkObject>()
            .SpawnAsPlayerObject(clientId);

        playerCount++;
    }
}
