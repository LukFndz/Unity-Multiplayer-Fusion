using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class NetworkPlayer : NetworkBehaviour
{
    public static NetworkPlayer Local { get; private set; }

    NicknameText _myNickname;

    [Networked(OnChanged = nameof(OnNicknameChanged))]
    NetworkString<_16> Nickname { get; set; }

    public event Action OnLeft = delegate { };

    public override void Spawned()
    {
        _myNickname = NicknameHandler.Instance.AddNickname(this);


        if (Object.HasInputAuthority)
        {
            Local = this;

            Debug.Log("[Custom Msg] ''Local'' Player Spawned");

            RPC_SetNickname("Pescador " + UnityEngine.Random.Range(1, 1001));
        }
        else
        {
            Debug.Log("[Custom Msg] Remote Player Spawned");
        }

    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    void RPC_SetNickname(string nick)
    {
        Nickname = nick;
    }

    static void OnNicknameChanged(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.UpdateNickname(changed.Behaviour.Nickname.ToString());
    }

    void UpdateNickname(string name)
    {
        _myNickname.UpdateText(name);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnLeft();
    }
}
