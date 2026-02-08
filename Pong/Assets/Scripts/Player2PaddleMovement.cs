using UnityEngine;

public class Player2PaddleMovement : PaddleController, ICollidable
{

    public void OnHit(Collision2D collision)
    {

        Debug.Log("Player 2 was hit!");
        // Look for the script on the object hit, or any of its parents
        BallMovement ball = collision.otherCollider.GetComponentInParent<BallMovement>();

        if (ball != null)
        {
            ball.Color = Color.yellow;
        }

    }
    protected override void Update()
    {

        GetComponent<Renderer>().material.color = Color.yellow;
        float vertical = Input.GetAxis("Player2Vertical");
        transform.position += new Vector3(0, vertical * speed * Time.deltaTime, 0);
    }
}
