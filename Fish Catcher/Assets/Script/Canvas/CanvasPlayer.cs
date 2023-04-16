using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fusion;


public class CanvasPlayer : NetworkBehaviour
{
    [SerializeField] private Timer _timerPrefab;
    [SerializeField] private Animator _animSelect;
    [SerializeField] private RectTransform _rectSelect;
    [SerializeField] private TMPro.TextMeshProUGUI _txtScore;

    private int _playerCount;

    public event Action OnUpdatePlayers = delegate { };

    public event Action OnEndTime = delegate { };

    public event Action OnUnlockInputs = delegate { };


    public void Start()
    {
        OnUpdatePlayers += SetGameCount;
    }

    public override void Spawned()
    {
        timer = 90;
    }

    public void SetPlayerInput(Player player)
    {
        OnUnlockInputs += player.UnlockInputs;
        OnEndTime += player.BlockInputs;
    }

    public void SetPlayerAnim(PlayerThrow player)
    {
        player.SetAnimSelect(_animSelect,_rectSelect, _txtScore);
    }

    private bool _startGame;
    private bool _endGame;
    
    [Networked]
    public float timer { get; set; }
    void LateUpdate()
    {
        if (_playerCount >= 2 && !_startGame)
        {
            _startGame = true;
            OnUnlockInputs();
        }

        if (_startGame && !_endGame)
        {
            //OnUpdateTime(_timer);
            if(Object.HasStateAuthority)
            {
                RPC_SetTimer(timer);
                timer -= Time.deltaTime;
            }
        }
    }

    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    void RPC_SetTimer(float time, RpcInfo info = default)
    {
        _timerPrefab.UpdateTimer(time);
    
        if(timer <= 0)
        {
            timer = 0;
            _timerPrefab.UpdateTimer(0);
            OnEndTime();
            _endGame = true;
            FindObjectOfType<ScoreManager>().ActiveWinnerCanvas();
        }
    }

    public void SetGameCount()
    {
        _playerCount++;
    }
}
