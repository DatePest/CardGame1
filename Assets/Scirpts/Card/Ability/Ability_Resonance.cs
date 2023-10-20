using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Resonance", menuName = "SO/AbilityBase/Ability_Resonance")]
public class Ability_Resonance : AbilityBase
{
    [SerializeField] Event_Ability_RemoveBuff _Removebuff;
    
    SO_Unit TargetU , MyUnit;
    public override void AddUseAbility(SO_Unit unit)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        unit.SetAtk_Resonance(true);
        MyUnit = unit;
        var UL = CardGame_Ctrl_Net.Instance.GetAllSelectRange(unit.CurrentOwner, TargetRange.Own);

        var Target = UL.Where(n => n.UnitData.UnitMaxHp == UL.Max(n => n.UnitData.UnitMaxHp)).FirstOrDefault();


        if(TargetU != unit)
        {
            TargetU = Target.UnitData;
            TargetU.UpdataAction += Resonance;
        }
        else
        {
            _Removebuff.IEvent(unit);
        }

    }

    public override void RemoveAbility(SO_Unit unit)
    {
        unit.SetAtk_Resonance(false);
        if (TargetU == null) return;
        TargetU.UpdataAction -= Resonance;
        TargetU = null;
        MyUnit.CallUpdata();
        MyUnit = null;
    }

    public void Resonance()
    {
        MyUnit.Resonance_SetLastAtk(TargetU.LastAtk);

    }
    public override void Stack(bool IsAdd)
    {
        return;
    }
}
