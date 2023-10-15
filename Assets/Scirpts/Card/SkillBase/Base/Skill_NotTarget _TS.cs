using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;


public abstract class Skill_NotTarget_TS : ActiveSkillBase
{
    [Header("TS_Tpye Need != Avg")]
    [SerializeField]
    TS_Tpye TSskillType;
    

    public TS_Tpye TSSkillType => TSskillType;
    [Header("‘ÅŠJŒã›’Žè“®‘I¢ŠYŽŸ“I")]
    [SerializeField] protected bool ManualSelect =false , IsCanNull = false;
    public bool Manual_Select => ManualSelect;
    protected TS_Tpye? ManualSelect_TSType =null;
    protected void SetTSskillType(TS_Tpye tS_Tpye)
    {
        ManualSelect_TSType = tS_Tpye;
    }
    //public abstract   Task UseSkill(AbilityNeedData data, TS_Tpye tS_Tpye,  CardSolt solt);

    public override bool PassiveReadySkillData(AbilityNeedData data)
    {
        if (data.AbilityValue1 == 0
            )
            return false;
        return true;
    }
    public async override Task<AbilityNeedData> UpSkillData(AbilityNeedData data)
    {
        if (ManualSelect == true)
        {
            data.CurrentUsePlayer.PlayerTrigger.SetActiveTS_Select(SetTSskillType, IsCanNull);
            while (ManualSelect_TSType == null)
            {
                await Task.Delay(300);
            }
            //B = UseSkill(data, (TS_Tpye)ManualSelect_TSType, data.CurrentUsePlayer.CurrentUseCard);
            data.AbilityValue1 = Ts_Int(ManualSelect_TSType.Value);
            ManualSelect_TSType = null;
        }
        else
        {
            //B = UseSkill(data, TSskillType, data.CurrentUsePlayer.CurrentUseCard);
            data.AbilityValue1 = Ts_Int(TSskillType);
        }
        
        return data;
    }
    public int Ts_Int(TS_Tpye TS)
    {
        //WEX, DEX, AVG
        switch (TS)
        {
            case TS_Tpye.WEX:
                    return 1;
            case TS_Tpye.DEX:
                    return 2;
            case TS_Tpye.AVG:
                return 3;
        }
        return 0;
    }
}


