using UnityEngine;

public class PaddleController : MonoBehaviour
{
    protected float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // float vertical = Input.GetAxis("Vertical");
        // transform.position += new Vector3(0, vertical * speed * Time.deltaTime, 0);
    }
}
