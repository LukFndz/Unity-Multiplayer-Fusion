using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Player : NetworkBehaviour
{
    private Vector3 _move;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    //[SerializeField] private CharacterController _controller;
    [SerializeField] private NetworkRigidbody _rbNet;
    [SerializeField] private GameObject _canvas;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rbNet = GetComponent<NetworkRigidbody>();
        CanvasManager.Instance.AddCanvas(_canvas);
    }

    public void Movement(NetworkInputData data)
    {
        Vector3 direction = _rbNet.Rigidbody.rotation * data.movementInput;
        if (data.movementInput.magnitude > 0.01)
            _rbNet.Rigidbody.MovePosition(_rbNet.Rigidbody.position + direction * Time.fixedDeltaTime * _speed);


        if (_turnSpeed > 0)
        {
            transform.localRotation = Quaternion.Euler(-data.rotationInput.y, data.rotationInput.x * _turnSpeed, 0);    
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            Movement(networkInputData);
        }
    }

    public void BlockInputs()
    {
        _speed = 0;
        _turnSpeed = 0;
    }

    public void UnlockInputs()
    {
        _speed = 5;
        _turnSpeed = 2;
    }
}
