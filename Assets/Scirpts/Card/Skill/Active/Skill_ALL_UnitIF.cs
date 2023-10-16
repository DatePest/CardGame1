using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "AddBuffALL_UnitIF ", menuName = "SO/CardSkill/IF/ALL_UnitIF")]
public sealed class Skill_ALL_UnitIF : Skill_NotTarget_All
{
    [SerializeField] List<Skill_NeedTargetBase> TurnSkills, FalseSkills;
    [Header("”@‰ÊŸ“—L ‘¥•Ô‰ñTrun")]
    [SerializeField] UnitCheckBase UnitCheck;
    Skill_ALL_UnitIF()
    {
        skill_Rule = Enum_Skill_Rule.Addbuff;
    }
    [SerializeField] bool SortFilterEnable =false;
    [SerializeField] bool IsSortMax=false;
    [SerializeField] int RunTimes=1;
    [SerializeField] CheckSortTpye SortTpye;


    public override async Task UseSkill(AbilityNeedData data)
    {
        //Debug.Log(AbilityID);
        var UList = CardGame_Ctrl_Net.Instance.GetAllSelectRange(data, target);
        int Times = UList.Count;
        if (SortFilterEnable)
        {
            UList = UListSortTpye(UList, IsSortMax, SortTpye);

            if (RunTimes > UList.Count)
                Times = UList.Count;
            else
                Times = RunTimes;

        }
        
       
       for(int i=0;i< Times;i++)
        {
            var u = UList[i];
            data.UserTarget = u;
            bool B;
            
            if (UnitCheck == null)
                B = true;
            else
                B = UnitCheck.UseCheck(u);
            //Debug.Log(u.UnitData.UnitName + B);
            if (B)
            {
                foreach (var a in TurnSkills)
                {
                    await a.UseSkill(data);
                }
            }
            else
            {
                foreach (var a in FalseSkills)
                {
                    await a.UseSkill(data);
                }
            }
                
        }
    }
}
