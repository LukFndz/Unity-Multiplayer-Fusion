using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[RequireComponent(typeof(NetworkCharacterControllerCustom))]
public class CharacterMovementHandler : NetworkBehaviour
{

    NetworkCharacterControllerCustom _myCharacterControllerCustom;
    Camera _camera;
    Canvas _canvas;

    public bool blockInput;


    private void Awake()
    {
        _myCharacterControllerCustom = GetComponent<NetworkCharacterControllerCustom>();
        _camera = GetComponentInChildren<Camera>();
        _canvas = GetComponentInChildren<Canvas>();
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

    public void CheckCamera()
    {
        if (Object.HasInputAuthority)
        {
            _canvas.gameObject.SetActive(true);
            _camera.gameObject.SetActive(true);
        }
    }
}
