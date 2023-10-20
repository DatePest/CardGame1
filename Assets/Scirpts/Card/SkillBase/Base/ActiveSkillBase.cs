using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;

public abstract class ActiveSkillBase : SO_SKillAbility
{
    protected MapShowTpye mapShow = MapShowTpye.Null;

    [SerializeField] protected bool Temp_SaveData = false, UseTemp_LoadData = false,LoadData_NotLoadMap = false;

    public abstract Task<AbilityNeedData> UpSkillData(AbilityNeedData data);
    public ActiveSkillBase()
    {
        skillType = SkillType.Active;
    }
    public async override void  Card_ReadySkill(CardSolt cardSolt, float Time = 1F, bool ifUseNotify = false)
    {
        var Base = await BaseReadySkill(cardSolt, Time);
        if (Base == null)
        {
            ReturnSkillCompleted(cardSolt);
            return;
        }
        AbilityNeedData data = Base.Value;
         await ReadySkillData(data);
        
         ReturnSkillCompleted(cardSolt);
    }
    public virtual async Task ReadySkillData(AbilityNeedData data)
    {
        var Updata = await UpSkillData(data);
        StartRunSkill(Updata);
    }
    protected async void StartRunSkill(AbilityNeedData data)
    {
        UseNotify(data);
        await UseSkill(data);
        await Task.Delay(300);
    }
    public async void PassiveSkillRunSkill(AbilityNeedData data)
    {
        if (PassiveReadySkillData(data))
        {
            await  UseSkill(data);
            await Task.Delay(300);
            return;
        }
        if(data.CurrentUsePlayer.OwnerClientId != NetworkManager.Singleton.LocalClientId) return;
        {
            CardGameManager.Instance.NeedWait = true;
            data.AbilityID = AbilityID;
            var Updata = await UpSkillData(data);
            if (CardGameManager.Instance.IsSingleplayer != true)
                CardGameManager.Instance.GameNotifyAction_Net.Unit_UseSkillServerRpc(new Net_AbilityNeedData(Updata));
            await  UseSkill(Updata);
            await Task.Delay(300);
            CardGameManager.Instance.NeedWait = false;
        }
      
    }
    public abstract bool PassiveReadySkillData(AbilityNeedData data);
    protected enum CheckSortTpye
    {
        Null, Hp, Atk, Armor
    }

    protected  void MouseShowType(PlayerOBJ player , MapShowTpye M)
    {
        player.UserMouseManager.SetMapShowTpye(M);
    }
    public  sealed override  void RemoveSkill()
    {
        //is null
    }
    /// Base  
    /// 
    protected async  Task<AbilityNeedData?> BaseReadySkill(CardSolt cardSolt, float Time)
    {
        AbilityNeedData data;
        if (UseTemp_LoadData && cardSolt.CurrentUsePlayer.BeforeData!=null)
        {
            
            data = new(cardSolt.CurrentUsePlayer.BeforeData.Value);
            if (LoadData_NotLoadMap == true)
                data.MapSolt = null;
        }
        else 
        {
            data = new();
        }
        data.SetBeseData(cardSolt, AbilityID);

        await Task.Delay((int)Time * 100);
        CheckDate Check;
        if (DisCard_Extra_SkillCheckList.Count > 0)
            Check = await SUseCheck(data, cardSolt, DisCard_Extra_SkillCheckList);
        else
            Check = await SUseCheck(data, cardSolt);
        if (!Check.UseCheck)
        {
            return null;
        }
        data.Use = Check.Extra;
        UseStartSkillAction(data, cardSolt);

        return data;
    }
    
    protected void UseNotify(AbilityNeedData data )
    {
        if (Temp_SaveData)
        {
            data.CurrentUsePlayer.BeforeData = data;
        }
        //if (ifUseNotify)
        //    CardGameManager.Instance.GameNotifyAction_Net.CardUseNotifyServerRpc(cardSolt.CardSO.CardId, cardSolt.CurrentInCardsPile());
        if (CardGameManager.Instance.IsSingleplayer != true)
            CardGameManager.Instance.GameNotifyAction_Net.Card_UseSkillServerRpc(new Net_AbilityNeedData(data));
    }

    public void ReturnSkillCompleted(CardSolt cardSolt)////all end
    {
        MouseShowType(cardSolt.CurrentUsePlayer,MapShowTpye.Null);
        if (cardSolt.DisCardUseingLook)
            cardSolt.DisCardCheckTimesADD(1);
        else if (cardSolt.CardUseingLook)
            cardSolt.CardCheckTimesADD(1);
    }
    public async Task<CheckDate> SUseCheck(AbilityNeedData data, CardSolt cardSolt, List<SO_CardCheckType> DisCard_Extra_SkillCheckList = null)
    {
        var checkDate = new CheckDate();
        checkDate.UseCheck = true;
        if (skillUseChecks.Count < 1)
        {
            return checkDate;
        }
        
        foreach (var a in skillUseChecks)
        {
            CheckDate B;
            if (DisCard_Extra_SkillCheckList != null)
                B = await a.UseCheck(data, cardSolt, DisCard_Extra_SkillCheckList);
            else
                B = await a.UseCheck(data, cardSolt);
            if (B.UseCheck == false)
            {
                checkDate.UseCheck = false;
            }
            if (B.Extra == true)
                checkDate.Extra = true;
        }
        return checkDate;
    }
   


    protected void UseStartSkillAction(AbilityNeedData data, CardSolt cardSolt)
    {
        cardSolt.CurrentUsePlayer.UseStartSkillAction(data, this);
        if (CardGameManager.Instance.IsSingleplayer != true)
            CardGameManager.Instance.GameNotifyAction_Net.UseStartSkillActionServerRpc(new Net_AbilityNeedData(data));
    }
    //////////////////////////////////////// Skill_NeedMapBase



    public MapSolt RandomMapTarget(PlayerOBJ CardUsePlayer, TargetRange RangeNumber, MapSelectType selectType, MapSelectCondition Condition)
    {
        var List = CardGame_Ctrl_Net.Instance.GetAllMapSolt();
        var NList = new List<MapSolt>();
        foreach (var a in List)
        {
            if (MapTargetRangeCheck(CardUsePlayer, a, RangeNumber))
                NList.Add(a);
        }
        var LastList = new List<MapSolt>();
        foreach (var a in NList)
        {
            if (MapCheck(a, Condition))
            {
                LastList.Add(a);
            }

        }
        if (selectType == MapSelectType.Random)
        {
            int R = Random.Range(0, LastList.Count - 1);
            return LastList[R];
        }
        return null;
    }


    public MapSolt WaitMapSolt(AbilityNeedData data, TargetRange RangeNumber, MapSelectCondition condition)
    {
        if (!CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().IsDisplayIsActive())
            CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().Show_Set(SelectTargetTpye.Map, RangeNumber);
        if (data.CurrentUsePlayer.UserMouseManager.CurrnetHits != null)
        {
            foreach (var result in data.CurrentUsePlayer.UserMouseManager.CurrnetHits)
            {
                if (result.transform.TryGetComponent<MapSolt>(out var T))
                {
                    if (T != null && MapTargetRangeCheck(data.CurrentUsePlayer, T, RangeNumber)&& MapCheck(T, condition))
                    {
                        CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().Close();
                        data.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
                        return T;
                    }
                    continue;
                }

            }
            data.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
        }

        return null;
    }

    public bool MapTargetRangeCheck(PlayerOBJ CardUsePlayer, MapSolt T, TargetRange RangeNumber)
    {
        if (RangeNumber == TargetRange.Own)
        {
            if (T.PlayerOwnerNumberID == CardUsePlayer.OwnerClientId) return true;
            return false;
        }
        else if (RangeNumber == TargetRange.Enemy)
        {
            if (T.PlayerOwnerNumberID != CardUsePlayer.OwnerClientId) return true;
            return false;
        }
        else
        {
            return true;
        }

    }

    public bool MapCheck(MapSolt mapSolt, MapSelectCondition Condition)
    {
        var T = mapSolt.GetUnit();
        switch (Condition)
        {
            case MapSelectCondition.IsNull:
                if (T == null)
                    return true;
                break;
            case MapSelectCondition.IsHaveUnit:
                if (T != null && T.UnitData.UnitTpye == UnitTpye.Hero || T.UnitData.UnitTpye == UnitTpye.Normal)
                    return true;
                break;
            case MapSelectCondition.IsHaveBarrier:
                if (T != null && T.UnitData.UnitTpye == UnitTpye.Barrier)
                    return true;
                break;
            case MapSelectCondition.IsHaveHero:
                if (T != null && T.UnitData.UnitTpye == UnitTpye.Hero)
                    return true;
                break;
            case MapSelectCondition.IsHaveAny:
                    return true;
                break;

        }
        return false;
    }
    ////////////////////////////////Skill_NeedTargetBase
    public bool UnitTargetRangeCheck(PlayerOBJ CardUsePlayer, SO_Unit T, TargetRange RangeNumber)
    {
        if (RangeNumber == TargetRange.Own)
        {
            if (T.PlayerOwnerNumberID == CardUsePlayer.OwnerClientId) return true;
            return false;
        }
        else if (RangeNumber == TargetRange.Enemy)
        {
            if (T.PlayerOwnerNumberID != CardUsePlayer.OwnerClientId) return true;
            return false;
        }
        else
        {
            return true;
        }

    }

    protected Unit RandomUnitTarget(PlayerOBJ CardUsePlayer, TargetRange RangeNumber, UnitSortSelectType selectType, CheckSortTpye SortTpye, List<UnitCheckBase> unitChecks1)
    {
        var Units = CardGame_Ctrl_Net.Instance.GetAllSelectRange(CardUsePlayer, RangeNumber);
        var LastList = new List<Unit>();
        foreach (var a in Units)
        {
            bool B = false;
            if (unitChecks1 == null ||unitChecks1.Count == 0 )
                LastList.Add(a);
            else
                foreach (var Check in unitChecks1)
                {
                    B = Check.UseCheck(a);
                    if (B == true)
                    {
                        LastList.Add(a);
                        break;
                    }

                }
        }
      
        if (selectType == UnitSortSelectType.Random)
        {
            int R =  Mathf.Max(0, Random.Range(0, LastList.Count - 1));
            return LastList[R];
        }
        else
            return SetSortTpye(LastList, selectType, SortTpye);
    }


    protected Unit WaitUnit(AbilityNeedData data, TargetRange RangeNumber, UnitTypeRange unitType , List<UnitCheckBase> Checks)
    {
        if (!CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().IsDisplayIsActive())
            CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().Show_Set(SelectTargetTpye.Unit, RangeNumber);
        if (data.CurrentUsePlayer.UserMouseManager.CurrnetHits != null)
        {
            foreach (var result in data.CurrentUsePlayer.UserMouseManager.CurrnetHits)
            {
                //Debug.Log("result.name = " + result.transform.gameObject.ToString());
                if (result.transform.TryGetComponent<MapSolt>(out var T))
                {
                    var U = T.GetUnit();
                    if (U != null && UnitTargetRangeCheck(data.CurrentUsePlayer, U.UnitData, RangeNumber))
                    {
                        /// only skill_Rule Attack
                        /// 
                        if(skill_Rule == Enum_Skill_Rule.Attack && data.UserTarget !=null)
                        {
                            if(data.UserTarget.UnitData.Focus == true)
                            {
                                CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().Close();
                                data.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
                                return T.GetUnit();
                            }

                            var TempU = T.GetUnit();
                            List<Unit> UList = new();
                            foreach(var a in T.MyMapArea.GetAllUnit())
                            {
                                if (a.UnitData.Cover == true)
                                    UList.Add(a);
                            }
                            if (UList.Count > 0)
                            {
                                foreach (var a in UList)
                                {
                                    if (TempU == a)
                                    {
                                        CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().Close();
                                        data.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
                                        return TempU;
                                    }
                                }
                                break;
                            }
                            
                        }
                        bool Ccheck = true;
                        foreach(var a in Checks)
                        {
                            Ccheck = a.UseCheck(U);
                            if(Ccheck != true)
                            {
                                break;
                            }
                        }
                        if( UnitTypeCheck(U, unitType) && Ccheck==true)
                        {
                            data.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
                            CardGame_PlayerUIManager.Instance.Get_SkillselectTooltip().Close();
                            return U;
                        }
                    }
                    continue;
                }

            }
            data.CurrentUsePlayer.UserMouseManager.CurrnetHits = null;
        }

        return null;
    }

    bool UnitTypeCheck(Unit u, UnitTypeRange unitType)
    {
        var T = u.UnitData.UnitTpye;
        switch (T)
        {
            case UnitTpye.Hero:
                if (unitType.Hero)
                    return true;
                break;
            case UnitTpye.Barrier:
                if (unitType.Barrier)
                    return true;
                break;
            case UnitTpye.Summoner:
                if (unitType.Summoner)
                    return true;
                break;
            default:
                break;

        }
        return false;
    }



    protected Unit SetSortTpye(List<Unit> LastList, UnitSortSelectType selectType, CheckSortTpye SortTpye)
    {
        switch (SortTpye)
        {
            case CheckSortTpye.Null:
                break;
            case CheckSortTpye.Hp:
                if (selectType == UnitSortSelectType.Max)
                    return LastList.Where(n => n.UnitData.UnitNowHp == LastList.Max(n => n.UnitData.UnitNowHp)).FirstOrDefault();
                else
                    return LastList.Where(n => n.UnitData.UnitNowHp == LastList.Min(n => n.UnitData.UnitNowHp)).FirstOrDefault();
            case CheckSortTpye.Atk:
                if (selectType == UnitSortSelectType.Max)
                    return LastList.Where(n => n.UnitData.LastAtk == LastList.Max(n => n.UnitData.LastAtk)).FirstOrDefault();
                else
                    return LastList.Where(n => n.UnitData.LastAtk == LastList.Min(n => n.UnitData.LastAtk)).FirstOrDefault();
            case CheckSortTpye.Armor:
                if (selectType == UnitSortSelectType.Max)
                    return LastList.Where(n => n.UnitData.LastArmor == LastList.Max(n => n.UnitData.LastArmor)).FirstOrDefault();
                else
                    return LastList.Where(n => n.UnitData.LastArmor == LastList.Min(n => n.UnitData.LastArmor)).FirstOrDefault();
        }
        throw new System.NotImplementedException();
        //return null;
    }

    protected List<Unit> UListSortTpye(List<Unit> LastList, bool IsMax, CheckSortTpye SortTpye)
    {
        
        switch (SortTpye)
            {
                case CheckSortTpye.Null:
                    break;
                case CheckSortTpye.Hp:
                if (IsMax)
                    return LastList.Where(n => n.UnitData.UnitNowHp == LastList.Max(n => n.UnitData.UnitNowHp)).ToList();
                else
                    return LastList.Where(n => n.UnitData.UnitNowHp == LastList.Min(n => n.UnitData.UnitNowHp)).ToList();
                case CheckSortTpye.Atk:
                if (IsMax)
                    return LastList.Where(n => n.UnitData.LastAtk == LastList.Max(n => n.UnitData.LastAtk)).ToList();
                else
                    return LastList.Where(n => n.UnitData.LastAtk == LastList.Min(n => n.UnitData.LastAtk)).ToList();
            case CheckSortTpye.Armor:
                if (IsMax)
                    return LastList.Where(n => n.UnitData.LastArmor == LastList.Max(n => n.UnitData.LastArmor)).ToList();
                else
                    return LastList.Where(n => n.UnitData.LastArmor == LastList.Min(n => n.UnitData.LastArmor)).ToList();
            }
        throw new System.NotImplementedException();
    }

}
