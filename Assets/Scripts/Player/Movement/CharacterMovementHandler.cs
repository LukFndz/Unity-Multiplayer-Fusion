using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[RequireComponent(typeof(NetworkCharacterControllerCustom))]
public class CharacterMovementHandler : NetworkBehaviour
{
    NetworkCharacterControllerCustom _myCharacterController;

    NetworkMecanimAnimator _myCharacterAnimator;

    float _movementValue;
    public bool blockInput;

    private void Awake()
    {
        blockInput = true;
        _myCharacterController = GetComponent<NetworkCharacterControllerCustom>();

        if (TryGetComponent(out LifeHandler myLifeHandler))
        {
            myLifeHandler.OnStateChange += SetControllerEnabled;
            myLifeHandler.OnRespawn += Respawn;
        }

        _myCharacterAnimator = GetComponent<NetworkMecanimAnimator>();
    }

    public override void FixedUpdateNetwork()
    {
        //if (Object.HasInputAuthority) return;

        if (GetInput(out NetworkInputData networkInputData))
        {

            //MOVIMIENTO
            if (!blockInput)
            {
                Vector3 moveDirection = Vector3.forward * networkInputData.movementInput;

                _myCharacterController.Move(moveDirection);

                //SALTO

                if (networkInputData.isJumpPressed)
                {
                    _myCharacterController.Jump();
                }
            }
                //ANIMATOR

                _movementValue = Mathf.Abs(_myCharacterController.Velocity.x);


                _myCharacterAnimator.Animator.SetFloat("MovementValue", _movementValue);
        }
    }

    void Respawn()
    {
        //Aquel que quiera agregar un Respawn con posiciones 'random' puede aplicar ese vector3 como parametro
        _myCharacterController.TeleportToPosition(transform.position);
        blockInput = false;
    }

    void SetControllerEnabled(bool isEnabled)
    {
        _myCharacterController.Controller.enabled = isEnabled;
    }
}
