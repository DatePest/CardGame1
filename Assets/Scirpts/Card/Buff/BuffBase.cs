using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBase : ScriptableObject
{
    public Sprite Icon;
    [SerializeField] string buffID ;
    public string BuffID => buffID;
    public string Description;
    public bool IsCanDispel = true , IsCanStack = false, IsUnique =false , IsHide = false;
    public int UniqueLevel = 0, StackTimes = 1;
    public BuffType buffTpye;
    public AbilityClass abilityClass;
    public List<AbilityBase> Abilitys;
    //public int NetId = -1;
    protected SO_Unit myunit;
    public BuffBase DeepCopy()
    {
        var C = Instantiate(this);
        C.Abilitys.Clear();
        foreach (var a in Abilitys)
        {
            C.Abilitys.Add(Instantiate(a));
        }
        return C;
    }
    public virtual void SetAndEnabledBuff(SO_Unit unit)
    {
        myunit = unit;
        OnBuffEnabled();
    }
    public abstract void OnBuffEnabled();
    public abstract void Interrupt_abstract();
    public void InterruptRemove()
    {
        Interrupt_abstract();
        Remove();
    }
    public virtual void Remove() 
    {
        myunit.RemoveBuff(this);
        myunit = null;
    }
    public virtual void Stack_(bool IsAdd)
    {
        if (IsAdd)
        {
            StackTimes++;
        }
        else
        {
            StackTimes--;
        }
    }
    public abstract void TimeAdd(int i);
    public void DynamicSet(int i)
    {
        foreach(var a in Abilitys)
        {
            if (a is AbilityValueAdded_Dynamic)
            {
                var b = a as AbilityValueAdded_Dynamic;
                b.SetAddedTimes(i, myunit);
                return;
            }
        }
    }
}

public enum BuffType
{
    Buff,DeBuff,Aura, SpecialUse

    //SpecialUse = 特殊機制使用 絕對不會被禁止添加
}
public enum AbilityClass
{
    Normal, //通用
    Shield,//護盾 //廢棄
    Cover,//掩護
    Focus //專注

}
