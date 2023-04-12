using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _isFishing;
    private bool _isInMinigame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && !_isFishing)
        {
            _animator.Play("Throw");
            _isFishing = true;
        }

    }
}
