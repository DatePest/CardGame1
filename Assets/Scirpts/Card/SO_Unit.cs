using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Unit", menuName = "SO/Character/SO_Unit")]
public class SO_Unit : ScriptableObject
{
    public ulong PlayerOwnerNumberID { get; private set; }
    public ulong UID { get; private set; }

    public Unit MyUnit { get; private set; }
    public PlayerOBJ CurrentOwner { get; private set; }
    public Sprite cardArt;

    public string  UnitName; //unitID >卡名 ,UnitName>規則上名稱
    [TextArea(4, 8)]
    public string UnitDepiction;
    [SerializeField]
    string unitID;
    [SerializeField]
    UnitTpye unitTpye;
    public Equipment Equipment { get; private set; }
    public UnitTpye UnitTpye => unitTpye;
    public string UintID => unitID;
    [SerializeField] CardAttributesType attributesType;
    public CardAttributesType AttributesType { get { return attributesType; } }
    public int UnitNowHp { get; private set; }
    [SerializeField]
    int BaseMaxHp, BaseAtk, BaseArmor;
    public int UnitMaxHp => BaseMaxHp;
    public int UnitAtk => BaseAtk;
    public int UnitArmor => BaseArmor;
    public int UnitShield { get; set; }
    int BaseMissRate = 0 , ModfirMissRate = 0;
    public int LastMissRate { get { return BaseMissRate + ModfirMissRate; } }
    int BaseDodgeRate = 0 , ModfirDodgeRate = 0;
    public int LastDodgeRate { get { return BaseDodgeRate + ModfirDodgeRate; } }

    public List<BuffBase> Buffs = new();
    public List<AbilityBase> Abilities = new();

    public List<SO_SKillAbility> Skills = new();
    public bool IsDead { get; private set; } = false;
    public bool IsBanAttack { get; private set; } = false;
    public bool IsBanAddbuff { get; private set; } = false;
    public bool Atk_Resonance { get; private set; } = false;
    public bool Blessing { get; private set; } = false;
    public bool Cover { get; private set; } = false;
    public bool Focus { get; private set; } = false;

    public event Action UpdataAction, NetCurrentHP;
    public event Action<SO_Unit, int> BePurified;
    public event Action<SO_Unit, BuffBase> BeAddBuff;
    public event Action<SO_Unit> Event_UintDead ,MoveEnd;
    public event Action<SO_Unit, SO_Unit,int> Event_CauseDamage;//攻擊單位(造成傷害單位),受到傷害單位(This)
    public event Action<SO_Unit, SO_Unit> Event_AtkStart, Event_BeAtkStart;//攻擊開始 Atkunit,BeUnit // BeUnit,AtkUnit
    public event Action<SO_Unit, SO_Unit, int> Event_BeDamage; //受到傷害(this ,attackUnit,d)
    //public event Action<BuffBase, bool> NetCurrentBuff;
    //public event Action<AbilityBase, bool> NetCurrentAbility;
    public event Func<int, SO_Unit, SO_Unit, AttackDamgerTpye, int> DamageModifier;
    public int ArmorAtk { get; private set; } = 0;
    public int ModfirAtk { get; private set; }
    public int ModfirArrmor { get; private set; }
    public int ModfirMaxHP { get; private set; }
    public float ModfirMultiplierAtk { get; private set; } = 1;
    public float ModfirMultiplierArrmor { get; private set; } = 1;
    public float ModfirMultiplierMaxHP { get; private set; } = 1;
    //public int ModfirHP { get; private set; }//可能會做成有限生命護盾之類的
    public int LastAtk { get ; private set; }
    public int LastArmor { get; private set; }
    public int LastMaxHP { get; private set; }

    public SO_Unit DeepCopy()
    {
        var C =Instantiate(this);
        C.Skills.Clear();
        foreach (var a in Skills)
        {
            var t = Instantiate(a);
            C.Skills.Add(t);
        }
        foreach (var a in Abilities)
        {
            var t = Instantiate(a);
            C.Abilities.Add(t);
        }
        return C;

    }
    public void SetCover(bool b)
    {
        Cover = b;
    }
    public void SetFocus(bool b)
    {
        Focus = b;
    }
    public void SetUnitTpye(UnitTpye tpye)
    {
        unitTpye = tpye;
    }
    public void SetIsBanAddbuff(bool b)
    {
        IsBanAddbuff = b;
    }
    public void SetIsBanAttack(bool b)
    {
        IsBanAttack = b;
    }
    public void SetIsBlessing(bool b)
    {
        Blessing = b;
    }
    
    public void SetModfirMissRate(int i)
    {
        ModfirMissRate += i;
    }
    public void SetModfirDodgeRate(int i)
    {
        ModfirDodgeRate += i;
    }
    public void Initialization(Unit U)
    {
        //UnitMaxHp = BaseMaxHp;
        //UnitAtk = BaseAtk;
       // UnitArmor = BaseArmor;
        MyUnit = U;
        UnitNowHp = UnitMaxHp;
        SetUID(U.UnitID);
        foreach (var a in Abilities)
        {
            a.AddUseAbility(this);
        }
        PassiveSkillStartAll();
    }
    public void SetUnitShield(int i)
    {
        UnitShield += i;
        //Debug.Log(UnitName + "sh=" + UnitShield);
        if (UnitShield < 0) UnitShield = 0;
        CallUpdata();
    }
    public void SetModfirAtk(int i)
    {
        ModfirAtk += i;
        CallUpdata();
    }

    public void SetModfirArrmor(int i)
    {
        ModfirArrmor += i;
        CallUpdata();
    }
    public void SetModfirMaxHP(int i)
    {
        ModfirMaxHP += i;
        CallUpdata();
    }
    public void SetModfirMultiplierAtk(float i, bool IsDivide = false)
    {
        if (IsDivide)
            ModfirMultiplierAtk /= i;
        else
            ModfirMultiplierAtk *= i;
        CallUpdata();
    }

    public void SetModfirMultiplierArrmor(float i, bool IsDivide = false)
    {
        if (IsDivide)
            ModfirMultiplierArrmor /= i;
        else
            ModfirMultiplierArrmor *= i;
        CallUpdata();
    }
    public void SetModfirMultiplierMaxHP(float i, bool IsDivide = false)
    {
        if (IsDivide)
            ModfirMultiplierMaxHP /= i;
        else
            ModfirMultiplierMaxHP *= i;
        CallUpdata();
    }

    public void SetArmorAtk(int i)
    {
        ArmorAtk += i;
        if (ArmorAtk < 0) ArmorAtk = 0;
        CallUpdata();
    }

    public void EventAtkStart(SO_Unit BeUnit)
    {
        Event_AtkStart?.Invoke(this, BeUnit);
    }
    public void EventCauseDamage(SO_Unit BeUnit,int D)
    {
        Event_CauseDamage?.Invoke(this, BeUnit,D);
    }
    public void TakeDamger(int Dmg, SO_Unit AtkUnit, UnitHitTpye hitTpye, AttackDamgerTpye damgerTpye = AttackDamgerTpye.Normal)
    {
        Debug.Log($"{unitID}受到的攻擊_"+Dmg +"Type= "+damgerTpye.ToString());
        if (hitTpye == UnitHitTpye.Miss)
        {
            if(AtkUnit !=null)
            Debug.Log($"{unitID}受到{AtkUnit.unitID}的攻擊 miss");
            else
                Debug.Log($"{unitID}受到的攻擊 miss");
            return;
        }
        
        int D = Dmg;
        if (damgerTpye != AttackDamgerTpye.Enforce) // Normal + Direct ok
        {
            if(damgerTpye != AttackDamgerTpye.Direct)
            {
                AtkUnit.EventAtkStart(this);
                Event_BeAtkStart?.Invoke(this, AtkUnit);
                var Mod_Ar = Math.Max((LastArmor - AtkUnit.ArmorAtk), 0);
                D = Math.Max(Dmg - Mod_Ar, 0);
            }
            
            if (DamageModifier != null)
            {
                var Last = DamageModifier?.Invoke(D, AtkUnit, this, damgerTpye);
                D = Last.Value;
                //var delegates = DamageModifier.GetInvocationList();
                //foreach (var del in delegates)
                //{
                //    //Func<int, SO_Unit, SO_Unit, AttackDamgerTpye, int> 
                //    var func = (Func<int, SO_Unit, SO_Unit, int>)del;
                //    D = func(D, AtkUnit, this); //AtkUnit , behurt
                //}
            }
        }
        
        D = Shield(D);
        //DamageModifier?.Invoke(D, AtkUnit, this, damgerTpye);
        UnitNowHp -= D;
        if (UnitNowHp <= 0)
        {
            UnitNowHp = 0;
            IsDead = true;
            RemoveAllBuff();
            Debug.Log("Dead");
            Event_UintDead?.Invoke(this);
        }
        //Debug.Log($"{unitID}受到{AtkUnit.unitID}的{D}傷害");
        if (damgerTpye == AttackDamgerTpye.Normal)//Normal only
        {
            if (AtkUnit != null)
            {
                AtkUnit.EventCauseDamage(this,D);//造成傷害單位 ,受到傷害單位
                Event_BeDamage?.Invoke(this, AtkUnit, D);
            }
            else
                Event_BeDamage?.Invoke(this, null, D);
        }
        CallUpdata();
    }
    int Shield(int D)
    {
        if (UnitShield < D)
        {
            UnitShield = 0;
            D -= UnitShield;
            return D;
        }
        else
        {
            UnitShield -= D;
            return 0;
        }
    }
    public void TakeHealHp(int i)
    {
        UnitNowHp += i;
        if (UnitNowHp >= LastMaxHP)
            UnitNowHp = LastMaxHP;
        CallUpdata();
    }
    public void SetHp(int i)
    {
        UnitNowHp = i;
        CallUpdata();
    }
    public void CallUpdata()
    {
        SetLastAtk();
        LastArmor = (int)((float) (UnitArmor + ModfirArrmor) * ModfirMultiplierArrmor);
        LastMaxHP = (int)Math.Max(1, (ModfirMaxHP + UnitMaxHp)* ModfirMultiplierMaxHP ) ;
        UpdataAction?.Invoke();
        //NetCurrentHP?.Invoke();
    }
    public void SetLastAtk()
    {
        if(!Atk_Resonance)
        LastAtk = (int)Math.Max(0, (ModfirAtk + UnitAtk) * ModfirMultiplierAtk);
    }
    public void Resonance_SetLastAtk(int I)
    {
        LastAtk = I;
    }
    public void SetAtk_Resonance(bool b)
    {
        Atk_Resonance = b;
    }
    public BuffBase AddBuff(BuffBase B)
    {
        if (B.buffTpye != BuffType. SpecialUse && IsBanAddbuff == true|| B==null) return null;
        if (B.buffTpye == BuffType.DeBuff && Blessing == true) return null;

        BuffBase New_Buff =null;
         if (B.IsCanStack)
        {
            foreach (var a in Buffs)
            {
                if (a.BuffID == B.BuffID)
                {
                    a.Stack_(true);
                    if (a.buffTpye != BuffType. SpecialUse)
                        BeAddBuff?.Invoke(this, a);
                    CallUpdata();
                    return a;
                }
            }
        }
        if (B.IsUnique)
        {
            foreach (var a in Buffs)
            {
                if (a.BuffID == B.BuffID)
                {
                    if (B.UniqueLevel >= a.UniqueLevel)
                    {
                        RemoveBuff(a);
                        break;
                    }
                    else
                    {
                        CallUpdata();
                        return null;
                    }
                }
            }
        }

        New_Buff = B.DeepCopy();
        Buffs.Add(New_Buff);
        foreach (var a in New_Buff.Abilitys)
        {
            //a.IsBuffAdd = true;
            Abilities.Add(a);
            a.AddUseAbility(this);
        }
        New_Buff.SetAndEnabledBuff(this);
        if(New_Buff.buffTpye != BuffType. SpecialUse)
        BeAddBuff?.Invoke(this, New_Buff);
        CallUpdata();

        return New_Buff;
        //B.NetId = Buffs.IndexOf(B);
        //NetCurrentBuff?.Invoke(B,true);
    }
    public void Purify(PurifyTpye purifyTpye)
    {
        int i = 0;
        List<BuffBase> T = new(Buffs);
        foreach (var a in T)
        {
            if (I_PurifyCheck.CheckType(a.buffTpye, purifyTpye))
            {
                RemoveBuff(a);
                i++;
            }  
        }

        BePurified?.Invoke(this, i);
    }

    public void Purify_Target(BuffBase B ,bool IsForceRemove)
    {
        if (B.IsCanDispel == false && IsForceRemove != true) return;
        RemoveBuff(B);
        BePurified?.Invoke(this, 1);
    }
    public void RemoveBuff(BuffBase B )
    {
        BuffBase Target =null;
        if (!Buffs.Contains(B))
        {
            foreach (var A in Buffs)
            {
                if (A.BuffID == B.BuffID)
                {
                    Target = A;
                    break;
                }
            }
        }
        else
            Target = B;
        if (Target == null) return;
        //if (Target.IsCanStack == true && Target.TimeRound > 0)
        //{
        //    Target.Stack_(false);

        //    if (Target.StackTimes > 0) return;
        //}

        Buffs.Remove(Target);
        foreach (var a in Target.Abilitys)
        {
            a.RemoveAbility(this);
        }
        CallUpdata();
        //NetCurrentBuff?.Invoke(B, false);
    }
    void RemoveAllBuff()
    {
        foreach(var a in Buffs)
        {
            foreach (var b in a.Abilitys)
            {
                b.RemoveAbility(this);
            }
        }
        Buffs.Clear();
    }
    public void AddAbility(AbilityBase ability)
    {
        Abilities.Add(ability);
        ability.AddUseAbility(this);
        CallUpdata();
        //if (ability.IsBuffAdd) return;
        //NetCurrentAbility?.Invoke(ability,true);
    }
    public void RemoveAbility(AbilityBase ability)
    {
        AbilityBase Target = null;
        if (!Abilities.Contains(ability))
        {
            foreach (var A in Abilities)
            {
                if (A.AbilityId == ability.AbilityId)
                {
                    Target = A;
                    break;
                }
            }
        }
        else
            Target = ability;
        if (Target == null) return;
        Abilities.Remove(ability);
        ability.RemoveAbility(this);
        CallUpdata();
        //NetCurrentAbility?.Invoke(ability,false);
    }

    public void SetOwner(ulong Id)
    {
        PlayerOwnerNumberID = Id;
        foreach(var a in CardGameManager.Instance.Players)
        {
            if(a.OwnerClientId== PlayerOwnerNumberID)
            {
                CurrentOwner = a;
                return;
            }

        }
    }
    public void SetUID(ulong Id)
    {
        UID = Id;
    }

    public void SetCallUpdataAction(Action action,bool B)
    {
        if (B == true) UpdataAction += action;
        else
            UpdataAction -= action;
    }
    public bool IfHaveBuff(BuffBase buff)
    {
        foreach (var a in Buffs)
        {
            if (a.BuffID == buff.BuffID)
                return true;
        }
        return false;
    }

    public void AddEquipment(Equipment E)
    {
        Equipment = E;
        Equipment.EnableEquipment(this);
    }
    public void RemoveEquipment()
    {
        if (Equipment == null) return;
        Equipment.DisEnableEquipment(this);
        Equipment = null;
    }
    public void PassiveSkillRemoveAll()
    {
        foreach (var a in Skills)
        {
            if (a.SkillType == SkillType.Passive)
                a.RemoveSkill();
        }
    }
    public void PassiveSkillStartAll()
    {
        AbilityNeedData data = new();
        data.UserTarget = MyUnit;
        data.CurrentUsePlayer = CurrentOwner;
        foreach (var a in Skills)
        {
            if (a.SkillType == SkillType.Passive)
                a.UseSkill(data);
        }
    }
    public void RemoveAll()
    {
        PassiveSkillRemoveAll();
        RemoveEquipment();
        RemoveAllBuff();
        for(int i=0; i< Abilities.Count; i++)
        {
            RemoveAbility(Abilities[0]);
        }
    }
   
    public void Move()
    {
        MoveEnd?.Invoke(this);
    }


    public void SetAttributesType(CardAttributesType Type)
    {
        attributesType = Type;
    }
    //public void COPY_SO(SO_Unit _Unit)
    //{
    //    this.cardArt = _Unit.cardArt;
    //    this.cardname = _Unit.cardname;
    //    this.UnitName = _Unit.UnitName;
    //    this.HeryType = _Unit.HeryType;
    //    this.cardAtk = _Unit.cardAtk;
    //    this.cardMaxHp = _Unit.cardMaxHp;
    //    this.cardAmmor = _Unit.cardAmmor;
    //    this.Abilities = _Unit.Abilities;
    //    this.Ability01 = _Unit.Ability01;
    //    this.Ability02 = _Unit.Ability02;
    //    this.Ability3 = _Unit.Ability3;
    //}

}
public enum AttackDamgerTpye
{
    Normal, Direct, Enforce
    ////Normal  普通 - 字面意思 普通 可被護甲減免 可被傷害修正  可觸發受到傷害與造成傷害 事件
    ////Direct  直接   部可被護甲減免 可被傷害修正  可觸發受到傷害事件 不會觸發造成傷害 事件
    ////Enforce 強制   不可被減免+修正 不會造成任何事件對應

}
public enum UnitTpye :byte
{
    Normal,Hero, Summoner, Barrier
}
public enum UnitHitTpye : byte
{
    Hit,Miss, Dodge
}
//public enum UnitEventTpye : byte
//{
//    MoveEnd,BeAddbuff,BePurified,
//}



