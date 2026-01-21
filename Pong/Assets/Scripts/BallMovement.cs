using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components
        Rigidbody2D ball = GetComponent<Rigidbody2D>();

        // Set initial values
        ball.linearVelocity = new Vector2(3.0f, 3.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
