using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CañaHandlerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerThrow _playerThrow;

    public void StartMiniGame()
    {
        _playerThrow.AnimatorSelect.gameObject.SetActive(true);
        _playerThrow.AnimatorSelect.Play("SelectMove");
        _playerThrow.StartMiniGame();
        _player.BlockInputs();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Limit")
        {
            Debug.Log("ACA");
            _playerThrow.OnZone = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Limit")
        {
            _playerThrow.OnZone = false;
        }
    }
}
