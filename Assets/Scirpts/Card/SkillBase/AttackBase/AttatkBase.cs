using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttatkBase 
{
    public AbilityNeedData SkillData { get; private set; }
    public abstract void AtkAction();
    public abstract List<MapSolt> GetMaps();
    public abstract void UIShow(BattleActionPool pool);

    public  AttatkBase(AbilityNeedData needData)
    {
        SkillData = needData;
    }
}
