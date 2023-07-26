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

    public event Action OnStartGame = delegate { };


    public void Start()
    {
        OnUpdatePlayers += SetGameCount;
    }

    public override void Spawned()
    {
        timer = 30;
    }


    Player _player;
    public void SetPlayerInput(Player player)
    {
        OnStartGame += player.SendToSpawnPoint;
        OnStartGame += player.BlockInputs;
        OnStartGame += StartTimer;
        OnEndTime += player.BlockInputs;
        _player = player;
    }

    public void SetPlayerAnim(PlayerThrow player)
    {
        player.SetAnimSelect(_animSelect,_rectSelect, _txtScore);
    }

    private bool _startGame;
    private bool _starTimer;
    private bool _endGame;
    
    public void StartTimer()
    {
        StartCoroutine(StartTimerRun());
    }
    IEnumerator StartTimerRun()
    {
        yield return new WaitForSeconds(3);
        _starTimer = true;
    }

    [Networked]
    public float timer { get; set; }
    void LateUpdate()
    {
        if (_endGame)
            _player.BlockInputs();

        if (_playerCount >= 2 && !_startGame)
        {
            timer = 30;
            _timerPrefab.UpdateTimer(30);
            _startGame = true;
            OnStartGame();
        }

        if (_startGame && !_endGame && _starTimer)
        {
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
