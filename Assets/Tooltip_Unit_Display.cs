using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip_Unit_Display : TooltipBase
{
    [SerializeField] UnitSolt unit_Display;
    [SerializeField] ObjPool pool;
    [SerializeField] TextMeshProUGUI equipment;
    public void Set_Show(Unit unit)
    {
        equipment.text = "";
        if (unit.UnitData.Equipment != null)
            equipment.text = unit.UnitData.Equipment.EquipmentName;

        ////////////////////
        BuffBox B = null;
        foreach (var a in pool.EnableList)
        {
             B = a.GetComponent<BuffBox>();
            B.Remove();
        }
        pool.RemoveAll();

        ////////////////////

        unit_Display.Tool_DisplayUnit(unit.UnitData);
        foreach(var a in unit.UnitData.Buffs)
        {
            if (a.IsHide) continue;
            pool.GetPool(out var g);
             B =g.GetComponent<BuffBox>();
            B.SetBox(a);
        }
        //Debug.Log("unit_Display_Buff__" + unit.UnitData.Buffs.Count);
        gameObject.SetActive(true);
    }
    
}
