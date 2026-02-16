using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Netcode;

public class BallMovement : NetworkBehaviour, ICollidable
{
    private float speed = 7f;
    private float maxSpeed = 20f;
    private Vector2 direction;
    private Rigidbody2D ball;
    private SpriteRenderer _spriteRenderer;
    private NetworkVariable<Color> netColor =
    new NetworkVariable<Color>(Color.white);



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
        get => netColor.Value;
        set
        {
            if (IsServer)
            {
                netColor.Value = value;
            }
        }

    }



    private void Awake()
    {
        ball = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        ApplyColor(netColor.Value);


        if (IsServer)
        {
            Direction = new Vector2(3.0f, 3.0f);
        }
    }
    void FixedUpdate()
    {
        if (!IsServer) return;
        ball.linearVelocity = direction * speed;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (!IsServer) return;
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
        if (!IsServer) return;
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


    private void OnEnable()
    {
        netColor.OnValueChanged += OnColorChanged;
    }

    private void OnDisable()
    {
        netColor.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        ApplyColor(newColor);
    }

    private void ApplyColor(Color c)
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = c;
        else
            GetComponent<Renderer>().material.color = c;
    }

}
