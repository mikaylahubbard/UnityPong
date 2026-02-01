using UnityEngine;

public class Player1PaddleMovement : PaddleController
{
    protected override void Update()
    {
        float vertical = Input.GetAxis("Player1Vertical");
        transform.position += new Vector3(0, vertical * speed * Time.deltaTime, 0);
    }
}
