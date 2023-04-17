using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Player : NetworkBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private NetworkRigidbody _rbNet;

    public float TurnSpeed { get => _turnSpeed; set => _turnSpeed = value; }
    public float Speed { get => _speed; set => _speed = value; }

    public event System.Action OnUpdateTimer = delegate { };

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rbNet = GetComponent<NetworkRigidbody>();
        BlockInputs();
    }

    public override void Spawned()
    {
        FindObjectOfType<CanvasPlayer>().SetGameCount();
        FindObjectOfType<CanvasPlayer>().SetPlayerInput(this);
    }

    public void Movement(NetworkInputData data)
    {
        Vector3 direction = _rbNet.Rigidbody.rotation * data.movementInput;
        if (data.movementInput.magnitude > 0.01)
        {
            _rbNet.Rigidbody.MovePosition(_rbNet.Rigidbody.position + direction * Time.fixedDeltaTime * _speed);
        }

        if (_turnSpeed > 0)
        {
            transform.localRotation = Quaternion.Euler(-data.rotationInput.y, data.rotationInput.x, 0);    
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
        GetComponent<PlayerThrow>().UnlockFish();
        _speed = 5;
        _turnSpeed = 2;
    }
}
