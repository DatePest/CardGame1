using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Additional_Attack ", menuName = "SO/CardSkill/Attack/Additional_Attack")]
public sealed class Skill_Additional_Attack: Skill_NotTarget
{
    [SerializeField]
    Range range;
    Skill_Additional_Attack()
    {
        skill_Rule = Enum_Skill_Rule.Attack;
    }
    Round_Battle B;

    public override async Task UseSkill(AbilityNeedData data)
    {
        if(B == null)
        {
            Round_IState T = null;
            CardGameManager.Instance.GameTurnSystem.GetRoundiState(Round_State_Enum.Round_Battle, ref T);
            B = T as Round_Battle;
        }
        B.Round_Action += EXAttack;
    }

    private void EXAttack()
    {
        if(B.Battle_Actions.Count > 0)
        {
            AttatkBase atk = null;
            switch (range)
            {
                case Range.First:
                    atk = B.Battle_Actions[0];
                    break;
                case Range.Last:
                    if(B.Battle_Actions.Count >1)
                        atk = B.Battle_Actions[^0];
                    else
                        atk = B.Battle_Actions[0];
                    break;
                case Range.Random:
                    int i = Random.Range(0, B.Battle_Actions.Count - 1);
                    atk = B.Battle_Actions[i];
                    break;
                default:
                    return;

            }
            B.BattleActionsAdd(atk);
        }
        
        B.Round_Action -= EXAttack;
    }

    public enum Range : byte
    {
        First,Last,Random
    }
}
