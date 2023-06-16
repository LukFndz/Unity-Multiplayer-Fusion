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
    public TMPro.TextMeshProUGUI timerUI;
    private float timer;


    private void Awake()
    {
        _myCharacterControllerCustom = GetComponent<NetworkCharacterControllerCustom>();
        _camera = GetComponentInChildren<Camera>();
        _canvas = GetComponentInChildren<Canvas>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        timer = 30;
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
        if (!Object.HasInputAuthority)
            return;

        StartCoroutine(CO_StartTime());
        _canvas.gameObject.SetActive(true);
        _camera.gameObject.SetActive(true);

    }

    IEnumerator CO_StartTime()
    {
        while (timer > 0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            timerUI.text = timer.ToString("N0");
        }

        foreach (var item in FindObjectsOfType<CharacterMovementHandler>())
        {
            item.blockInput = true;
        }

    }
}
