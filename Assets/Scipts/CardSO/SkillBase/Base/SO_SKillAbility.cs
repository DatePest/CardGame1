using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using System.Threading.Tasks;
[System.Serializable]
public abstract class SO_SKillAbility : ScriptableObject
{
    [SerializeField]
    string abilityID;
    public string AbilityID { get => abilityID; }
    [TextArea(4,8)]
    public string SkillDepiction;
    [Header("主動:使用時候//被動:不生效")]
    [SerializeField]
    public List<SkillUseCheckBase> skillUseChecks;
    [SerializeField]
    [Header("捨棄比對//Data中的EXUSE僅卡片技能空組有實現//被動時候不啟用")]
    protected List<SO_CardCheckType> DisCard_Extra_SkillCheckList;
    [SerializeField]
    protected Enum_Skill_Rule skill_Rule = Enum_Skill_Rule.Null;
    public Enum_Skill_Rule Skill_Rule => skill_Rule;
     protected SkillType skillType;
    public SkillType SkillType => skillType;
    //public abstract IEnumerator ReadySkillCoroutine( CardSolt cardSolt,float Time = 2F, bool ifUseNotify =false);
    public abstract void  Card_ReadySkill(CardSolt cardSolt, float Time = 2F, bool ifUseNotify = false);
   
    public abstract  Task UseSkill(AbilityNeedData data);
    public abstract  void RemoveSkill();


    //延續使用data //是否保存 到玩家腳本上  // 取用後執行 卡片使用完畢後馬上要消除
}
public enum SkillType
{
    Active, Passive
}
public enum MapSelectType
{
    Manual, Random
}
public enum UnitSortSelectType
{
    Manual, Random, Max, Small
}

public enum TargetRange
{
    Enemy, Own, All
}


//public void UseSkillNotify_NetServer(AbilityNeedData Data)
//{
//    Net_AbilityNeedData N = new(Data.AbilityID,
//        Data.UserTarget.NetworkObjectId,
//        Data.AbilityToTarget.NetworkObjectId,
//        Data.CurrentUsePlayer.NetworkObjectId,
//        Data.AbilityValue1,
//        Data.AbilityValue2,
//        Data.AbilityValue3,
//        Data.AbilityValue4);
//    UseSkillServerRpc(N);
//}

public struct AbilityNeedData
{
    public int UseCardUid;
    public string AbilityID;
    public int[] SelectCardUids;
    public Unit UserTarget, AbilityToTarget; // unit Uid  //UserTarget 是卡片使用目標
    public MapSolt MapSolt;
    public PlayerOBJ CurrentUsePlayer;
    public int AbilityValue1, AbilityValue2, AbilityValue3, AbilityValue4; //AbilityValue4 Use If A or B
    public bool Use;
    // AbilityToTarget
    public AbilityNeedData(Net_AbilityNeedData NetData)
    {
        if (CardGameManager.Instance.UnitDictionary[NetData.UserTargetUnitID] != null)
            UserTarget = CardGameManager.Instance.UnitDictionary[NetData.UserTargetUnitID];
        else
            UserTarget = null;
        if (CardGameManager.Instance.UnitDictionary[NetData.AbilityToTargetUnitID] != null)
            AbilityToTarget = CardGameManager.Instance.UnitDictionary[NetData.AbilityToTargetUnitID];
        else
            AbilityToTarget = null;
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(NetData.CurrentUsePlayerID, out NetworkObject NetObject3))
            CurrentUsePlayer = NetObject3.GetComponent<PlayerOBJ>();
        else
            CurrentUsePlayer = null;
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(NetData.MapSoltID, out NetworkObject  NetObject4))
            MapSolt = NetObject4.GetComponent<MapSolt>();
        else
            MapSolt = null;
        AbilityID = NetData.Ability_ID;
        UseCardUid = NetData.UseCardUid;
        AbilityValue1 = NetData.AbilityValue1;
        AbilityValue2 = NetData.AbilityValue2;
        AbilityValue3 = NetData.AbilityValue3;
        AbilityValue4 = NetData.AbilityValue4;
        Use = NetData.ExUse;
        if (NetData.SelectCardUids != null)
        {
            SelectCardUids = new int[NetData.SelectCardUids.Length];
            SetSelectCardId(NetData.SelectCardUids);
        }
        else
            SelectCardUids = null;
    }
    public AbilityNeedData(AbilityNeedData data)
    {
        UseCardUid = data.UseCardUid;

        if (data.AbilityID != null)
            AbilityID = data.AbilityID;
        else
            AbilityID = new("");
        if (data.UserTarget != null)
            UserTarget = data.UserTarget;
        else
            UserTarget = null;
        if (data.AbilityToTarget != null)
            AbilityToTarget = data.AbilityToTarget;
        else
            AbilityToTarget = null;
        if (data.CurrentUsePlayer != null)
            CurrentUsePlayer = data.CurrentUsePlayer;
        else
            CurrentUsePlayer = null;
        if (data.MapSolt != null)
            MapSolt = data.MapSolt;
        else
            MapSolt = null;

        AbilityValue1 = data.AbilityValue1;
        AbilityValue2 = data.AbilityValue2;
        AbilityValue3 = data.AbilityValue3;
        AbilityValue4 = data.AbilityValue4;
        Use = data.Use;


        if (data.SelectCardUids != null)
        {
            SelectCardUids = new int[data.SelectCardUids.Length];
            SetSelectCardId(data.SelectCardUids);
        }
        else
            SelectCardUids = null;

    }

    public void SetSelectCardId(int[] List)
    {
        SelectCardUids = new int[List.Length];
        for (int i = 0; i < List.Length; i++)
        {
            SelectCardUids[i] = List[i];
        }
    }
    public void SetBeseData(CardSolt cardSolt,string CardAbilityID)
    {
        CurrentUsePlayer = cardSolt.CurrentUsePlayer;
        AbilityID = CardAbilityID;
        UseCardUid = cardSolt.CardUid;
        cardSolt.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
    }

}

public struct Net_AbilityNeedData : INetworkSerializable, IEquatable<Net_AbilityNeedData>
{
    public int UseCardUid;
    public NetworkString Ability_ID ;
    public int[] SelectCardUids;
    public ulong UserTargetUnitID, AbilityToTargetUnitID , CurrentUsePlayerID , MapSoltID; // unit Uid  //UserTarget 是卡片使用目標
    public int AbilityValue1, AbilityValue2, AbilityValue3, AbilityValue4;
    public bool ExUse;
    // AbilityToTarget
    public Net_AbilityNeedData(AbilityNeedData data)
    {
        UseCardUid = data.UseCardUid;
        if (data.AbilityID != null)
            Ability_ID = new NetworkString(data.AbilityID);
        else
            Ability_ID = new("");
        if (data.UserTarget != null)
            UserTargetUnitID = data.UserTarget.UnitID;
        else
            UserTargetUnitID = (ulong)0;
        if (data.AbilityToTarget != null)
            AbilityToTargetUnitID = data.AbilityToTarget.UnitID;
        else
            AbilityToTargetUnitID = (ulong)0;
        if (data.CurrentUsePlayer != null)
            CurrentUsePlayerID = data.CurrentUsePlayer.NetworkObjectId;
        else
            CurrentUsePlayerID = (ulong)0;
        if (data.MapSolt != null)
            MapSoltID = data.MapSolt.NetworkObjectId;
        else
            MapSoltID = (ulong)0;
    
        AbilityValue1 = data.AbilityValue1;
        AbilityValue2 = data.AbilityValue2;
        AbilityValue3 = data.AbilityValue3;
        AbilityValue4 = data.AbilityValue4;
        ExUse = data.Use;


        if (data.SelectCardUids != null)
        {
            SelectCardUids = new int[data.SelectCardUids.Length];
            SetSelectCardId(data.SelectCardUids);
        }
        else
            SelectCardUids = new int[0];

    }
    public void SetSelectCardId(int[] List)
    {
        for(int i=0;i< List.Length; i++)
        {
            SelectCardUids[i] = List[i];
        }
    }

    public bool Equals(Net_AbilityNeedData other)
    {
        return Ability_ID == other.Ability_ID && UserTargetUnitID == other.UserTargetUnitID && AbilityToTargetUnitID == other.AbilityToTargetUnitID;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Ability_ID);
        serializer.SerializeValue(ref UseCardUid);
        serializer.SerializeValue(ref SelectCardUids);
        serializer.SerializeValue(ref UserTargetUnitID);
        serializer.SerializeValue(ref AbilityToTargetUnitID);
        serializer.SerializeValue(ref CurrentUsePlayerID);
        serializer.SerializeValue(ref MapSoltID);
        serializer.SerializeValue(ref AbilityValue1);
        serializer.SerializeValue(ref AbilityValue2);
        serializer.SerializeValue(ref AbilityValue3);
        serializer.SerializeValue(ref AbilityValue4);
        serializer.SerializeValue(ref ExUse);
    }
}
