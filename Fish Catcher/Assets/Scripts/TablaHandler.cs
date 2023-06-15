using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablaHandler : MonoBehaviour
{
    [SerializeField] private PlayerThrow _playerThrow;
    public void PutFish()
    {
        _playerThrow.SetScoreUI();
        _playerThrow.AnimatorTabla.gameObject.SetActive(false);
        GetComponentInParent<CharacterMovementHandler>().blockInput = false;
    }
}
