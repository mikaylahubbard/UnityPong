using UnityEngine;

public abstract class PaddleController : MonoBehaviour
{
    protected float speed = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    protected abstract void Update();
}
