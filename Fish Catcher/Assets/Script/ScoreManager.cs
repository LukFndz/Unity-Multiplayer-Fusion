using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using System.Linq;

public class ScoreManager : NetworkBehaviour
{
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] WinnerTXT _txtWinner;


    [Networked]
    public int actualWinnerScore { get; set; }

    [Networked]
    public string actualWinnerName { get; set; } = "Default";

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SetScore(string nick, int score, RpcInfo info = default)
    {
        if(Object.HasStateAuthority)
        {
            if (score > actualWinnerScore)
            {
                actualWinnerScore = score;
                actualWinnerName = nick;
                RPC_SetWinner(actualWinnerName);
            }
        }

        Debug.Log(actualWinnerScore);

    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SetWinner(string nick, RpcInfo info = default)
    {
        _txtWinner.UpdateWinner(actualWinnerName);
    }

    public void ActiveWinnerCanvas()
    {
        _panelGameOver.SetActive(true);
    }

}
