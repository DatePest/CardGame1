using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;
using System;

public class Unit : MonoBehaviour
{
    public SO_Unit UnitData { get; private set; }
    public ulong UnitID { get; private set; }
    public MapSolt CurrentMapSolt { get; private set; }

    public void Set_InitializationUnit (SO_Unit unit, ulong ID, ulong Uid)
    {
        UnitData = unit;
        //UnitData.SetUID(NetworkObjectId);
        UnitData.SetOwner(ID);
        UnitID = Uid;
        //Debug.Log("UnitID" + UnitID);
        UnitData.Initialization(this);
        if(UnitData.UnitTpye == UnitTpye.Hero)
        UnitData.Event_UintDead += HeroDead;
        UnitData.Event_UintDead += UnitDead;

    }
    public void FindSkill(Net_AbilityNeedData data)
    {
        AbilityNeedData D = new(data);
        //Debug.Log("FindSkill");
        //Debug.Log("D.AbilityID = " + D.AbilityID);
        foreach (var a in GameManager.Instance.DataBase.SKills)
        {
            if(a.AbilityID == D.AbilityID)
            {
               
                a.UseSkill(D);
                
                return;
            }
        }
        
        //foreach(var a in UnitData.Skills)
        //{
        //    if(a.AbilityID == data.AbilityID)
        //    {
        //        return;
        //    }
        //}
    }

    private void HeroDead(SO_Unit unit)
    {
        Debug.Log("HeroDead");
        unit.CurrentOwner.cardSpawnScript.HeroDeadRemove(unit);
        CardGameManager.Instance.Hero_WinCheck(UnitData.CurrentOwner);
        unit.Event_UintDead -= HeroDead;
    }

    private  void UnitDead(SO_Unit unit)
    {
        unit.RemoveAll();
        CurrentMapSolt.RemoveUnit();
        gameObject.transform.parent = null;
        Destroy(gameObject);
        unit.Event_UintDead -= UnitDead;
        Debug.Log("UnitDead");
    }
    public void Move(MapSolt TargetMap)
    {
        Remove();
        TargetMap.SetUnit(this);;
        UnitData.Move();//call event
    }
    public void Remove()
    {
        if (CurrentMapSolt == null) return;
        CurrentMapSolt.RemoveUnit();
        CurrentMapSolt = null;
    }
    public  void SetCurrentMapSolt(MapSolt m)
    {
        CurrentMapSolt = m;
    }

    //[ServerRpc]
    //public void Net_CreateUnitServerRpc(string id , ServerRpcParams serverRpcParams = default)
    //{
    //    Net_CreateUnitClientRpc(id);
    //}
    //[ClientRpc]
    // void Net_CreateUnitClientRpc(string id)
    //{
    //    var U = GameManager.Instance.DataBase.StringToUnit(id);
    //    SetUnit(U);
    //}
    //[ServerRpc]
    //public void SetUnit_UnitTpyeServerRpc(UnitTpye unitTpye)
    //{
    //    SetUnit_UnitTpyeClientRpc(unitTpye);
    //}
    //[ClientRpc]
    // void SetUnit_UnitTpyeClientRpc(UnitTpye unitTpye)
    //{
    //    UnitData.SetUnitTpye(unitTpye);
    //}


    //private void SynchronizeCurrentAbility(AbilityBase ability, bool IsAdd)
    //{
    //    if (IsAdd)
    //        SynchronizeAbilityAddClientRpc(ability.AbilityId);
    //    else
    //        SynchronizeAbilityRemoveClientRpc(ability.AbilityId);
    //}


    //private void SynchronizeCurrentBuff(BuffBase Buff, bool IsAdd)
    //{
    //    if (IsAdd)
    //        SynchronizeBuffAddClientRpc(Buff.BuffID);
    //    else
    //        SynchronizeBuffRemoveClientRpc(Buff.NetId);
    //}

    //private void SynchronizeCurrentHP()
    //{
    //    SynchronizeHPClientRpc(UnitData.cardNowHp);
    //}
    //[ClientRpc]
    //void SynchronizeHPClientRpc(int Hp)
    //{
    //    if (IsServer || IsHost) return;
    //    UnitData.SetHp(Hp);
    //}
    //[ClientRpc]
    //void SynchronizeBuffAddClientRpc(string Buff)
    //{
    //    if (IsServer || IsHost) return;
    //    var B = GameManager.Instance.CardData.StringToBuff(Buff);
    //    UnitData.AddBuffAbility(B);
    //}
    //[ClientRpc]
    //void SynchronizeBuffRemoveClientRpc(int Buffid)
    //{
    //    if (IsServer || IsHost) return;
    //    var B = UnitData.Buffs.Where(n => n.NetId== Buffid).SingleOrDefault();
    //    UnitData.RemoveBuffAbility(B);
    //}
    //[ClientRpc]
    //private void SynchronizeAbilityAddClientRpc(string ability)
    //{
    //    if (IsServer || IsHost) return;
    //    var A = GameManager.Instance.CardData.StringToAbilityBase(ability);
    //    UnitData.AddAbility(A);
    //}
    //[ClientRpc]
    //private void SynchronizeAbilityRemoveClientRpc(string ability)
    //{
    //    if (IsServer || IsHost) return;
    //    var A = UnitData.Abilities.Where(n => n.AbilityId == ability).SingleOrDefault();
    //    UnitData.RemoveAbility(A);
    //}
}
