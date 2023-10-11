using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;

[CreateAssetMenu(fileName = "Skill_TS_ADD", menuName = "SO/CardSkill/TS_Add")]
public sealed class Skill_TS : Skill_NotTarget_TS
{
   
    [SerializeField] int TS_Times;
    Skill_TS()
    {
        skill_Rule = Enum_Skill_Rule.TS;
    }

    /// TsTimes < 5 = WEX   TsTimes > 5 =DEX 
    //public override async Task UseSkill(AbilityNeedData data, TS_Tpye tS_Tpye, CardSolt solt)
    //{
    //    if (TSSkillType == TS_Tpye.DEX)
    //        CardGameManager.Instance.Ts_Manager.SetValueTsTimes(+TS_Times);
    //    else if (TSSkillType == TS_Tpye.WEX)
    //        CardGameManager.Instance.Ts_Manager.SetValueTsTimes(-TS_Times);
    //    else if (tS_Tpye == TS_Tpye.AVG)
    //        return;
    //}

    public override async Task UseSkill(AbilityNeedData data)
    {
        //if(ManualSelect == true)
        //{
        //    //if (data.CurrentUsePlayer.OwnerClientId != NetworkManager.Singleton.LocalClientId) return;
        //    CardGameManager.Instance.NeedWait = true;
        //    data.CurrentUsePlayer.PlayerTrigger.SetActiveTS_Select(SetTSskillType, IsCanNull);
        //    while (ManualSelect_TSType == null)
        //    {
        //        await Task.Delay(400);
        //    }
        //    if (ManualSelect_TSType == TS_Tpye.DEX)
        //        CardGameManager.Instance.GameNotifyAction_Net.Cmd_TSadd_ServerRpc(+TS_Times);
        //    else if (ManualSelect_TSType == TS_Tpye.WEX)
        //        CardGameManager.Instance.GameNotifyAction_Net.Cmd_TSadd_ServerRpc(-TS_Times);
        //    CardGameManager.Instance.NeedWait = false;
        //    ManualSelect_TSType = null;
        //}
        //else
        //{
        //    if (TSSkillType == TS_Tpye.DEX)
        //        CardGameManager.Instance.Ts_Manager.SetValueTsTimes(+TS_Times);
        //    else if (TSSkillType == TS_Tpye.WEX)
        //        CardGameManager.Instance.Ts_Manager.SetValueTsTimes(-TS_Times);
        //}
        
        //if (data.CurrentUsePlayer.OwnerClientId != NetworkManager.Singleton.LocalClientId) return;

        switch (data.AbilityValue1)
        {
            case 1:
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(-TS_Times);
                return;
            case 2:
                CardGameManager.Instance.Ts_Manager.SetValueTsTimes(+TS_Times);
                return;
            case 3:
                return ;
        }

    }
}
