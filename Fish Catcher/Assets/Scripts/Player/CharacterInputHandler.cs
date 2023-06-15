using UnityEngine;

//Clase que va a tomar los inputs del jugador siempre y cuando tenga autoridad para hacerlo
public class CharacterInputHandler : MonoBehaviour
{
    Vector3 _move;
    Quaternion _rotation;

    bool _isFishingPressed;
    bool _isInteractPressed;

    NetworkInputData _inputData;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;


    void Start()
    {
        _inputData = new NetworkInputData();
    }
    Vector3 rotate = Vector3.zero;
    void Update()
    {

        //MOVIMIENTO

        _move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        Vector3 movement = Vector3.zero;

        movement += transform.forward * _move.z * _moveSpeed * Time.deltaTime;
        movement += transform.right * _move.x * _moveSpeed * Time.deltaTime;

        _move = movement;

        //ROTACION

        rotate.x += Input.GetAxis("Mouse X") * _turnSpeed;

        rotate.y += Input.GetAxis("Mouse Y") * _turnSpeed;

        _rotation = Quaternion.Euler(0, rotate.x, 0);

        //PESCA E INTERACT

        if (Input.GetMouseButtonDown(0))
        {
            _isFishingPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _isInteractPressed = true;
        }
    }

    public NetworkInputData GetNetworkInputs()
    {
        _inputData.movementInput = _move;
        _inputData.rotationInput = _rotation;

        _inputData.isFishingPressed = _isFishingPressed;
        _isFishingPressed = false;

        _inputData.isInteractPressed = _isInteractPressed;
        _isInteractPressed = false;

        return _inputData;
    }
}
