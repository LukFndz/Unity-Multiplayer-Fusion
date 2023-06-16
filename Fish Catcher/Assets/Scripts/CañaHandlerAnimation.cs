using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CañaHandlerAnimation : MonoBehaviour
{
    [SerializeField] private FishHandler _playerThrow;

    public void StartMiniGame()
    {
        _playerThrow.AnimatorSelect.gameObject.SetActive(true);
        _playerThrow.AnimatorSelect.Play("SelectMove");
    }
}
