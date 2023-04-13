using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Player _player;
    private Vector3 _move;
    private Vector2 _rot;
    private bool _isFishing;
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.HasInputAuthority) return;   

        _move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rot.x += Input.GetAxis("Mouse X");
        _rot.y += Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(0))
            _isFishing = true;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData _data = new NetworkInputData();

        _data.movementInput = _move;
        _data.rotationInput = _rot;
        _data.isFishPressed = _isFishing;
        _isFishing = false;
        
        return _data;
    }
}
