using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SO_Equipment", menuName = "SO/Equipment/Equipment")]
public class Equipment : ScriptableObject
{
    [SerializeField]
    string equipmentName;
    public string EquipmentName => equipmentName;
    [SerializeField]
    string equipmentID;
    public Sprite Icon;
    public string EquipmentID=> equipmentID;
    [TextArea(3,3)]
    public string Depiction;

    [SerializeField]
    List<PassiveSkillBase> equipmentSkill;
    public List<PassiveSkillBase> EquipmentSkill => equipmentSkill;
    public Equipment DeepCopy()
    {
        var C = Instantiate(this);
        C.equipmentSkill.Clear();
        foreach (var a in equipmentSkill)
        {
            C.equipmentSkill.Add(Instantiate(a));
        }
        return C;
    }


    public void EnableEquipment(SO_Unit U)
    {
        var data = new AbilityNeedData();
        data.UserTarget = U.MyUnit;
        foreach(var a in equipmentSkill)
        {
            a.UseSkill(data);
        }
    }
    public void DisEnableEquipment(SO_Unit U)
    {
        var data = new AbilityNeedData();
        data.UserTarget = U.MyUnit;
        foreach (var a in equipmentSkill)
        {
            a.RemoveSkill();
        }
    }

}
