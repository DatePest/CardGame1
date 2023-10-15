using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Netcode;

public class GameNotifyAction_Net  : NetworkBehaviour
{
    public event Action<CardSolt, float> CardUse;
    public event Action<Unit, float> Unit;
    public event Action CardEXUse;


    [ServerRpc(RequireOwnership = false)]
    public void UnitSkillNotifyServerRpc(ulong UID, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        float Time = 1f;
        UnitSkillNotifyClientRpc(UID, clientId, Time);
    }
    [ClientRpc]
    void UnitSkillNotifyClientRpc(ulong UID, ulong clientId, float Time)
    {
        var G = CardGameManager.Instance.UnitDictionary[UID];
        if (G == null) return;

        Unit?.Invoke(G, Time);
    }


    [ServerRpc(RequireOwnership = false)]
    public void CardUseNotifyServerRpc(int CardId ,CardsPileEnum CardsPile, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        float Time = 1f;
        CardUseNotifyServerClientRpc(CardId, clientId, CardsPile, Time);
    }
    [ClientRpc]
    void CardUseNotifyServerClientRpc(int CardId, ulong clientId,  CardsPileEnum From, float Time)
    {
        var G = CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId].FindCard(CardId, From);
        if (G == null) return;
       
        CardUse?.Invoke(G.GetComponentInChildren<CardSolt>(), Time);
    }
    [ServerRpc(RequireOwnership = false)]
    public void CardUseEndNotifyServerRpc(int CardId, CardsPileEnum From, CardsPileEnum Target, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        CardUseEndNotifyClientRpc(CardId, clientId, From, Target);
    }
    [ClientRpc]
    void CardUseEndNotifyClientRpc(int CardId, ulong clientId, CardsPileEnum From, CardsPileEnum Target)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId) return;
        var G = CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId].FindCard(CardId, From);
        if (G == null) return;
        CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId].CardsPileChange(G, From, Target);
    }

    [ServerRpc(RequireOwnership = false)]
    public void CardExUseEndServerRpc()
    {
        NotifyCardEXUseClientRpc();
    }
    [ClientRpc]
    void NotifyCardEXUseClientRpc()
    {
        CardEXUse?.Invoke();
    }


    //[ClientRpc]
    //public void StartInstantiateDeckClientRpc( NetworkString[] Dards , int CardSpawnManager, int start)
    //{
    //    int A = 0;
    //    foreach(var a in Dards)
    //    {
    //        CardGameManager.Instance.CardSpawnManager.CardSpawnScripts[CardSpawnManager].InstantiateGoto(CardsPileEnum.deck, start+A, Dards[A]);
    //        A++;
    //    }


    //    // for (int i=0;i< CardGameManager.Instance.CardSpawnManager.CardSpawnScripts.Length; i++)
    //    //{

    //    //     CardGameManager.Instance.CardSpawnManager.CardSpawnScripts[i].InstantiateGoto_for(CardsPileEnum.deck,DeckConut[i],0);
    //    //}
    //}
    [ServerRpc(RequireOwnership = false)]
    public void ExCost_SetValueTsTimesServerRpc(int UseCardUid, bool IsEx1, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        ExCost_SetValueTsTimesClientRpc(clientId, UseCardUid, IsEx1);
    }
    [ClientRpc]
     void ExCost_SetValueTsTimesClientRpc(ulong clientId, int UseCardUid, bool IsEx1)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId) return;
        var G = CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId].FindCardAll(UseCardUid);
        var so = G.GetComponentInChildren<CardSolt>();
        so.ExCost_SetValueTsTimes(IsEx1);
    }
    [ServerRpc(RequireOwnership = false)]
    public void UseStartSkillActionServerRpc(Net_AbilityNeedData NetData, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        UseStartSkillClientRpc(clientId, NetData);
    }
    [ClientRpc]
     void UseStartSkillClientRpc(ulong clientId, Net_AbilityNeedData NetData)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId) return;
        var G = CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId].FindCardAll(NetData.UseCardUid);
        var so = G.GetComponentInChildren<CardSolt>();
        so.NetUseStartSkill(NetData);
    }
    [ServerRpc(RequireOwnership = false)]
    public void Unit_UseSkillServerRpc(Net_AbilityNeedData NetData, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        Unit_UseSkillClientRpc(clientId, NetData);
    }
    [ClientRpc]
    void Unit_UseSkillClientRpc(ulong clientId, Net_AbilityNeedData NetData)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId) return;
        var U = CardGameManager.Instance.UnitDictionary[NetData.UserTargetUnitID];
        //var so = G.GetComponentInChildren<CardSolt>();
        U.FindSkill(NetData);
    }
    [ServerRpc(RequireOwnership = false)]
    public void Card_UseSkillServerRpc(Net_AbilityNeedData NetData, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        Card_UseSkillClientRpc(clientId, NetData);
    }
    [ClientRpc]
     void Card_UseSkillClientRpc(ulong clientId, Net_AbilityNeedData NetData)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId) return;
        var G = CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId].FindCardAll(NetData.UseCardUid);
        var so = G.GetComponentInChildren<CardSolt>();
        so.NetUseSkill(NetData);
    }
    [ServerRpc(RequireOwnership = false)]
    public void UseCheckDisCardServerRpc(Net_AbilityNeedData NetData, CardsPileEnum From, CardsPileEnum Target, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        UseCheckDisCardClientRpc(clientId, NetData, From, Target);
    }
    [ClientRpc]
     void UseCheckDisCardClientRpc(ulong clientId, Net_AbilityNeedData NetData, CardsPileEnum From, CardsPileEnum Target)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId) return;
        AbilityNeedData data = new (NetData);
        CardGameManager.Instance.CardSpawnManager.Dictionary_CardSpawnScripts[(byte)clientId].FindCardGoto(data.SelectCardUids, From, Target);
    }
    [ServerRpc]
    public void Cmd_UnitAtkUnit_ServerRpc(int Dmg, ulong AtkUnit, ulong DmgUnit, UnitHitTpye hitTpye, AttackDamgerTpye damgerTpye)
    {
        Cmd_UnitAtkUnit_ClientRpc(Dmg, AtkUnit, DmgUnit, hitTpye, damgerTpye);
    }
    [ClientRpc]
    void Cmd_UnitAtkUnit_ClientRpc(int Dmg, ulong AtkUnit, ulong DmgUnit, UnitHitTpye hitTpye, AttackDamgerTpye damgerTpye)
    {
        var U = CardGameManager.Instance.UnitDictionary[DmgUnit];
        var A = CardGameManager.Instance.UnitDictionary[AtkUnit];
        U.UnitData.TakeDamger(Dmg, A.UnitData, hitTpye, damgerTpye);
    }
    [ServerRpc]
    public void Cmd_UnitAbilityUse_ServerRpc(ulong Unit, ulong AbilityID)
    {
        Cmd_UnitAbilityUse_ClientRpc(Unit, AbilityID);
    }
    [ClientRpc]
     void Cmd_UnitAbilityUse_ClientRpc(ulong Unit, ulong AbilityID)
    {
        //var U = CardGameManager.Instance.UnitDictionary[Unit];
        
        //foreach(var a in U.UnitData.Abilities)
    }
    [ServerRpc(RequireOwnership = false)]
    public void Cmd_TSadd_ServerRpc(int t)
    {
        Cmd_TSadd_ClientRpc(t);
    }
    [ClientRpc]
    void Cmd_TSadd_ClientRpc(int t)
    {
        CardGameManager.Instance.Ts_Manager.SetValueTsTimes(t);
    }


}
