using UnityEngine;

public class Player1PaddleMovement : PaddleController, ICollidable
{
    public void OnHit(Collision2D collision)
    {
        Debug.Log("Player 1 was hit!");
        // Look for the script on the object hit, or any of its parents
        BallMovement ball = collision.otherCollider.GetComponentInParent<BallMovement>();

        if (ball != null)
        {
            ball.Color = Color.red;
        }
    }

    protected override void Update()
    {
        GetComponent<Renderer>().material.color = Color.red;
        float vertical = Input.GetAxis("Player1Vertical");
        transform.position += new Vector3(0, vertical * speed * Time.deltaTime, 0);
    }
}
