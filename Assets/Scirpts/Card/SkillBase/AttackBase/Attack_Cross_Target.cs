using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Attack_Cross_Target : AttatkBase
{
    public Attack_Cross_Target(AbilityNeedData data) : base(data)
    {
    }
    public override void AtkAction()
    {
        List<Unit> U = new();

        var List = GetMaps();
        foreach (var T in List)
        {
          
            if (T.Unitsolt.My_Unit != null)
            {
                var o = T.Unitsolt.My_Unit;
                if (SkillData.UserTarget.UnitData.PlayerOwnerNumberID != o.UnitData.PlayerOwnerNumberID)
                    U.Add(o);
            }
        }
        foreach (var a in U)
        {
            int D = SkillData.UserTarget.UnitData.LastAtk;
            var H = DiceSystem.HitDice(SkillData.UserTarget, a);
            CardGameManager.Instance.GameNotifyAction_Net.Cmd_UnitAtkUnit_ServerRpc(D, SkillData.UserTarget.UnitID, a.UnitID, H, AttackDamgerTpye.Normal);
            
        }

    }

    public override List<MapSolt> GetMaps()
    {
        return SkillData.MapSolt.MyMapArea.GetRangeCrossDirections(SkillData.MapSolt);
    }

    public override void UIShow(BattleActionPool pool)
    {
        pool.AttackUnitIcon.sprite = SkillData.UserTarget.UnitData.cardArt;
        pool.GUIText.text = "十字攻擊";
        pool.DamgerUnitIcon.sprite = null;
    }
}
