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

    public event Action<float> OnUpdateTime = delegate { };

    public event Action OnUnlockInputs = delegate { };


    public void Start()
    {

        OnUpdatePlayers += SetGameCount;

        //OnUpdateTime += _timerPrefab.UpdateTimer;
    }

    public override void Spawned()
    {
        timer = 30;
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
    
    [Networked]
    public float timer { get; set; }
    void LateUpdate()
    {
        if (_playerCount >= 2 && !_startGame)
        {
            _startGame = true;
            OnUnlockInputs();
        }

        if (_startGame)
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
    }

    public void SetGameCount()
    {
        _playerCount++;
    }
}
