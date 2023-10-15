using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Attack_Normal_ToTarget : AttatkBase
{
    public Attack_Normal_ToTarget(AbilityNeedData data) : base(data)
    {  
    }
    public override void AtkAction()
    {
        int D = SkillData.UserTarget.UnitData.LastAtk;
        var H = DiceSystem.HitDice(SkillData.UserTarget, SkillData.AbilityToTarget);
        CardGameManager.Instance.GameNotifyAction_Net.Cmd_UnitAtkUnit_ServerRpc(D, SkillData.UserTarget.UnitID, SkillData.AbilityToTarget.UnitID, H, AttackDamgerTpye.Normal);
    }

    public override List<MapSolt> GetMaps()
    {
        List<MapSolt> maps = new();
        maps.Add(SkillData.UserTarget.CurrentMapSolt);
        return maps;
    }

    public override void UIShow(BattleActionPool pool)
    {
        pool.AttackUnitIcon.sprite = SkillData.UserTarget.UnitData.cardArt;
        pool.GUIText.text = "單體攻擊";
        pool.DamgerUnitIcon.sprite = SkillData.AbilityToTarget.UnitData.cardArt;
    }

}
