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
            if(score == actualWinnerScore)
                RPC_SetWinner("Draw");

            if (score > actualWinnerScore)
            {
                actualWinnerScore = score;
                actualWinnerName = nick;
                RPC_SetWinner("Winner is " + actualWinnerName);
            }


            Debug.Log("SCORE: " + score);
            Debug.Log("ACTUAL WINNER: " + actualWinnerScore);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SetWinner(string nick, RpcInfo info = default)
    {
        _txtWinner.UpdateWinner(nick);
    }

    public void ActiveWinnerCanvas()
    {
        _panelGameOver.SetActive(true);
    }

}
