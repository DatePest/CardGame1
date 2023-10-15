using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Skill_NeedMapBase : ActiveSkillBase
{
    [SerializeField]
    protected MapSelectType SelectType1;
    [SerializeField]
    protected MapSelectCondition condition;
    [SerializeField]
    protected TargetRange targetRange_1;
    [SerializeField] protected MapShowTpye MapShow = MapShowTpye.Null;

    


}




public enum MapSelectCondition
{
    IsHaveAny, IsHaveHero, IsHaveUnit, IsHaveBarrier, IsNotBarrier, IsNull
}
