using UnityEngine;

//Clase que va a tomar los inputs del jugador siempre y cuando tenga autoridad para hacerlo
public class CharacterInputHandler : MonoBehaviour
{
    float _horizontalMovement;
    bool _isJumpPressed;
    bool _isFirePressed;

    NetworkInputData _inputData;

    void Start()
    {
        _inputData = new NetworkInputData();
    }

    void Update()
    {
        //Tomo todos los Inputs

        _horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _isJumpPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _isFirePressed = true;
        }
    }

    public NetworkInputData GetNetworkInputs()
    {
        _inputData.movementInput = _horizontalMovement;

        _inputData.isJumpPressed = _isJumpPressed;
        _isJumpPressed = false;

        _inputData.isFirePressed = _isFirePressed;
        _isFirePressed = false;

        return _inputData;
    }
}
