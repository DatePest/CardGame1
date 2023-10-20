using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Purify", menuName = "SO/CardSkill/Purify")]

public sealed class Skill_Purify : Skill_UserTarget
{
    [SerializeField] PurifyTpye purifyTpye;
    Skill_Purify()
    {
        skill_Rule = Enum_Skill_Rule.Heal;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        data.UserTarget.UnitData.Purify(purifyTpye);
    }
}
public enum PurifyTpye
{
    DeBuff, Buff, All
}

public static class I_PurifyCheck
{
    public static bool CheckType(BuffType buffTpye , PurifyTpye purifyTpye)
    {
        if (buffTpye == BuffType.SpecialUse) return false;
        if (purifyTpye == PurifyTpye.All) return true;
        if (purifyTpye == PurifyTpye.Buff)
            if (buffTpye == BuffType.Buff)
                return true;
            else
                return false;
        if (purifyTpye == PurifyTpye.DeBuff)
            if (buffTpye == BuffType.DeBuff)
                return true;
            else
                return false;
        return false;
    }
}
