using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovement : MonoBehaviour
{
    private float speed = 7f;
    private float maxSpeed = 20f;
    private Vector2 direction;
    private Rigidbody2D ball;


    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > maxSpeed)
            {
                speed = maxSpeed;
            }
            else if (value < 0)
            {
                speed = 0;
            }
            else
            {
                speed = value;
            }
        }
    }

    public Vector2 Direction
    {
        get { return direction; }
        set
        {
            direction = value.normalized;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components
        ball = GetComponent<Rigidbody2D>();

        // Set initial values
        Direction = new Vector2(3.0f, 3.0f);

    }

    void FixedUpdate()
    {
        ball.linearVelocity = direction * speed;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "vertical")
        {
            Direction = new Vector2(-Direction.x, Direction.y);
        }
        else if (collision.gameObject.tag == "horizontal")
        {
            Direction = new Vector2(Direction.x, -Direction.y);
        }
        else if (collision.gameObject.tag == "paddle")
        {
            Direction = new Vector2(-Direction.x, Direction.y);
        }

        ball.linearVelocity = direction * speed;
    }
}
