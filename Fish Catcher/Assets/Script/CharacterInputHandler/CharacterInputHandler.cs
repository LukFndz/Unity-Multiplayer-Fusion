using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : NetworkBehaviour
{
    Player _player;
    private Vector3 _move;
    private Vector2 _rot;
    private bool _isFishing;


    NetworkInputData _data;

    void Start()
    {
        _data = new NetworkInputData();
        _data.movementInput = Vector3.zero;
        _data.isFishPressed = false;
        _data.rotationInput = Vector3.zero;
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.HasInputAuthority) return;   

        _move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rot.x += Input.GetAxis("Mouse X") * _player.TurnSpeed;
        _rot.y += Input.GetAxis("Mouse Y") * _player.TurnSpeed;

        _rot.y = Mathf.Clamp(_rot.y, -20, 20);

        if (Input.GetMouseButton(0))
            _isFishing = true;
    }

    public NetworkInputData GetNetworkInput()
    {
        _data.movementInput = _move;
        _data.rotationInput = _rot;
        _data.isFishPressed = _isFishing;
        _isFishing = false;
        
        return _data;
    }
}