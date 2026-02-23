using UnityEngine;
using Unity.Netcode;
public class Checkpoint : MonoBehaviour
{

    // 1 = Player 1 (left) scores
    // 2 = Plater 2 (right) scores
    public int scoreZoneNumber;

    void OnTriggerEnter2D(Collider2D other)
    {

        // Check if a race car entered
        if (other.CompareTag("ball"))
        {

            GameManager manager = FindFirstObjectByType<GameManager>();
            manager.RecordScore(scoreZoneNumber);
        }
    }
}