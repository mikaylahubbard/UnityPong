using UnityEngine;

public class Player2PaddleMovement : PaddleController
{
    protected override void Update()
    {
        float vertical = Input.GetAxis("Player2Vertical");
        transform.position += new Vector3(0, vertical * speed * Time.deltaTime, 0);
    }
}
