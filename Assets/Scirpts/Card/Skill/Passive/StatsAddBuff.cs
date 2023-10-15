using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveStatsAddBuff", menuName = "SO/CardSkill/PassiveSkillBase/StatsAddBuff")]
public class StatsAddBuff : PassiveSkillBase
{
    
    [SerializeField]  List<BuffBase> Buffs;
    [SerializeField] List<AbilityBase> abilities;
    [Header("·¢ìÆödà ûäåèämîF")]
    [SerializeField] List<UnitCheckBase> Check;
    bool IsAddIng=false;
    [SerializeField] PassiveSkillSwitch PassivelSwitch;
    public override void RemoveSkill()
    {
        if (PassivelSwitch != null)
        {
            CardGameManager.Instance.Ts_Manager.TS_UpData_Action -= AEvent;
        }
        Remove();

        Data = null;

    }

    private void AEvent(int obj)
    {
        SwitchEvent(PassivelSwitch.CheckTs());
    }

    public override Task UseSkill(AbilityNeedData data)
    {
        Data = data;
        if (PassivelSwitch != null)
        {
            CardGameManager.Instance.Ts_Manager.TS_UpData_Action += AEvent;

            SwitchEvent(PassivelSwitch.CheckTs());
        }
        else
            Add();


        return null;
    }

    private void SwitchEvent(bool obj)
    {
        if (obj)
            Add();
        else
            Remove();
    }


    void Add()
    {
        if (IsAddIng) return;
        foreach (var a in Check)
        {
            var T = a.UseCheck(Data.Value.UserTarget);
            if (T != true)
                return ;
        }
        IsAddIng = true;
        foreach (var Buff in Buffs)
        {
            var B = Data.Value.UserTarget.UnitData.AddBuff(Buff);
            B.IsCanDispel = false;
            B.UniqueLevel++;
        }
        foreach (var a in abilities)
        {
            Data.Value.UserTarget.UnitData.AddAbility(a);
        }
    }

    void Remove()
    {
        if (!IsAddIng) return;
        IsAddIng = false;
        if (Data != null)
        {
            var b = Data.Value;
            foreach (var Buff in Buffs)
            {
                b.UserTarget.UnitData.RemoveBuff(Buff);
            }
            foreach (var a in abilities)
            {
                b.UserTarget.UnitData.RemoveAbility(a);
            }
        }
    }
}
