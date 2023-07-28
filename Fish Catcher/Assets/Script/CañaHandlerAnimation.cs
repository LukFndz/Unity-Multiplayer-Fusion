using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CañaHandlerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerThrow _playerThrow;

    public void StartMiniGame()
    {
        _playerThrow.StartMiniGame();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Limit")
        {
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
