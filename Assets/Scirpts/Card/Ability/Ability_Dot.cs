using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability_Dot", menuName = "SO/AbilityBase/Ability_Dot")]
public class Ability_Dot : AbilityBase
{
    [SerializeField]
     int Dmg ;
    Round_End_Event E;
    SO_Unit U;
    int StackT;
    public override void AddUseAbility(SO_Unit unit)
    {
        //if (!NetworkManager.Singleton.IsHost || !NetworkManager.Singleton.IsServer) return;
        Round_IState T = null;
        U = unit;
        CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_End_Event, ref T);
         E = T as Round_End_Event;
        StackT = 0;
        E.Round_Action += DamgerDot;
    }

    public override void RemoveAbility(SO_Unit unit)
    {
        if(E!=null) E.Round_Action -= DamgerDot;
        StackT = 0;
        E = null;
    }

    public void DamgerDot()
    {
        U.TakeDamger(Dmg +(StackT* Dmg), null,UnitHitTpye.Hit,AttackDamgerTpye.Enforce);
    }
    public override void Stack(bool IsAdd)
    {
        if (IsAdd)
            StackT++;
        else
            StackT--;
        if (StackT < 0)
            StackT = 0;
    }
}
