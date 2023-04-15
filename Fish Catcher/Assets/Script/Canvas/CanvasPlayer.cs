using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fusion;


public class CanvasPlayer : MonoBehaviour
{
    [SerializeField] private Timer _timerPrefab;
    [SerializeField] private Animator _animSelect;
    [SerializeField] private RectTransform _rectSelect;
    [SerializeField] private TMPro.TextMeshProUGUI _txtScore;
    [SerializeField] private float _timer;

    private int _playerCount;

    public event Action OnUpdatePlayers = delegate { };

    public event Action<float> OnUpdateTime = delegate { };

    public event Action OnUnlockInputs = delegate { };


    public void Start()
    {
        _timer = 30;

        OnUpdatePlayers += SetGameCount;

        OnUpdateTime += _timerPrefab.UpdateTimer;
    }

    public void SetPlayerInput(Player player)
    {
        OnUnlockInputs += player.UnlockInputs;
    }

    public void SetPlayerAnim(PlayerThrow player)
    {
        player.SetAnimSelect(_animSelect,_rectSelect, _txtScore);
    }

    private bool _startGame;
    
    void LateUpdate()
    {
        if (_playerCount >= 2 && !_startGame)
        {
            _startGame = true;
            OnUnlockInputs();
        }

        if (_startGame)
        {
            _timer -= Time.deltaTime;
            OnUpdateTime(_timer);
        }
    }

    public void SetGameCount()
    {
        _playerCount++;
    }
}
