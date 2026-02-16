using UnityEngine;
using Unity.Netcode;
using System.Linq.Expressions;
// LEFT paddle
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
        Debug.Log($"IsOwner: {IsOwner} | IsSpawned: {IsSpawned}");
        if (!CanProcessNetwork()) return;
        float currentY = yPosition.Value;
        GetComponent<Renderer>().material.color = Color.red;

        if (IsLocalOwner())
        {
            float input = Input.GetAxis("Player1Vertical");
            float newY = transform.position.y + (input * speed * Time.deltaTime);
            yPosition.Value = newY;
            // transform.position += new Vector3(0, vertical * speed * Time.deltaTime, 0);
            transform.position = new Vector3(transform.position.x, newY, 0);
        }
        else
        {
            // Non-owners: Read NetworkVariable and update visual position
            transform.position = new Vector3(transform.position.x, yPosition.Value, 0);
        }

    }
}
