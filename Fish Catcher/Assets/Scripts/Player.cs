using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _move;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private CharacterController _controller;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    Vector2 rotate;
    void Update()
    {

        Vector3 rawAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        rotate.x += Input.GetAxis("Mouse X") * _turnSpeed;
        
        rotate.y += Input.GetAxis("Mouse Y") * _turnSpeed;

        Vector3 movement = Vector3.zero;

        _move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        movement += transform.forward * _move.z * _speed * Time.deltaTime;
        movement += transform.right * _move.x * _speed * Time.deltaTime;
  

        transform.localRotation = Quaternion.Euler(0, rotate.x,0);
        if(rawAxis.magnitude > 0.01f)
        {
            _controller.Move(movement);
        }
    }
}
