using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector3 movementInput;
    public Quaternion rotationInput;
    public NetworkBool isFishingPressed;
    public NetworkBool isInteractPressed;
}
