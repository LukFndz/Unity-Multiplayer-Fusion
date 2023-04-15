using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocalHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public void ActiveCamera()
    {
        _camera.gameObject.SetActive(true);
    }
}
