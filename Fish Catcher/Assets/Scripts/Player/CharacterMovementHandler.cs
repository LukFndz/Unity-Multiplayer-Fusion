using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[RequireComponent(typeof(NetworkCharacterControllerCustom))]
public class CharacterMovementHandler : NetworkBehaviour
{

    NetworkCharacterControllerCustom _myCharacterControllerCustom;


    public bool blockInput;


    private void Awake()
    {
        _myCharacterControllerCustom = GetComponent<NetworkCharacterControllerCustom>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority)
            return;

        if (GetInput(out NetworkInputData networkInputData))
        {
            if (!blockInput)
            {
                //MOVIMIENTO
                _myCharacterControllerCustom.Move(networkInputData.movementInput);

                transform.localRotation = networkInputData.rotationInput;
            }
        }
    }
}
