using UnityEngine;
using Unity.Netcode;

public abstract class PaddleController : NetworkBehaviour
{
    protected float speed = 6f;
    protected NetworkVariable<float> yPosition =
    new NetworkVariable<float>(
        0f,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);


    protected bool CanProcessNetwork()
    {
        return IsSpawned;
    }
    protected bool IsLocalOwner()
    {
        return IsOwner;
    }

    // Update is called once per frame
    protected abstract void Update();
}
