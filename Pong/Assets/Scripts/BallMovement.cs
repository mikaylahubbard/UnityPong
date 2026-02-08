using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovement : MonoBehaviour, ICollidable
{
    private float speed = 7f;
    private float maxSpeed = 20f;
    private Vector2 direction;
    private Rigidbody2D ball;
    private SpriteRenderer _spriteRenderer;
    private Color color = Color.white;



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

    public Color Color
    {
        get { return color; }
        set
        {
            color = value;
            if (_spriteRenderer != null)
                _spriteRenderer.color = color;
            else
                GetComponent<Renderer>().material.color = color;
        }

    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components
        ball = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

        // call OnHit() for the object the ball collided with
        ICollidable collidable = collision.gameObject.GetComponent<ICollidable>();

        if (collidable != null)
        {
            collidable.OnHit(collision);
        }

        // call the ball's OnHit
        OnHit(collision);
    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log("Car was hit by: " + collision.gameObject.name);
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
