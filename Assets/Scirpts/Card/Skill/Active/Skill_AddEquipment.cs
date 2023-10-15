using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "AddEquipment", menuName = "SO/CardSkill/Add/AddEquipment")]
public sealed class Skill_AddEquipment : Skill_UserTarget
{
    [SerializeField] Equipment equipment;
    Skill_AddEquipment()
    {
        skill_Rule = Enum_Skill_Rule.Equipment;
    }
    public async Task I_UseSkill(AbilityNeedData data)
    {
        await UseSkill(data);
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        data.UserTarget.UnitData.AddEquipment(equipment.DeepCopy());
    }
}
