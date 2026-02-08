using UnityEngine;

public abstract class NetworkedObject : MonoBehaviour
{
    protected abstract void Initialize();
    protected abstract void GetNetworkID();
}