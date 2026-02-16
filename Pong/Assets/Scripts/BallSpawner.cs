using UnityEngine;
using Unity.Netcode;

public class BallSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        SpawnBall();
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        ball.GetComponent<NetworkObject>().Spawn();
    }
}
