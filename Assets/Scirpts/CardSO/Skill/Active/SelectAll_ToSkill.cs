using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectAll_ToSkill", menuName = "SO/CardSkill/SelectAll_ToSkill")]
public sealed class SelectAll_ToSkill : Skill_NotTarget_All
{
    [SerializeField] List<UnitCheckBase> UnitCheck ;
    //[Header("Manual>>Random")]
    [SerializeField] List<Skill_UserTarget> Skills;
    SelectAll_ToSkill()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        var UList = CardGameManager.Instance.GetAllSelectRange(data, target);
        AbilityNeedData Ndata = new(data);
        bool T_continue;
        foreach (var a in UList)
        {
            T_continue = false;
            foreach(var c in UnitCheck)
            {
                if(c.UseCheck(a) != true)
                {
                    T_continue = true;
                    break;
                }
            }

            if (T_continue)
            {
                continue;
            }

            Ndata.UserTarget = a;
            foreach (var b in Skills)
            {
                await b.UseSkill(Ndata);
            }
        }

    }
}
