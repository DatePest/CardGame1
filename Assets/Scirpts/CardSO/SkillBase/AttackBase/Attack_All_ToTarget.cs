using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Attack_All_ToTarget : AttatkBase
{
    public Attack_All_ToTarget(AbilityNeedData data) : base(data)
    {
        
    }
    public override void AtkAction()
    {
        List<Unit> U= new();

        var Map = GetMaps();

        foreach (var T in Map)
        {
            if (T.Unitsolt.My_Unit != null)
            {
                var o = T.Unitsolt.My_Unit;
                if(SkillData.UserTarget.UnitData.PlayerOwnerNumberID != o.UnitData.PlayerOwnerNumberID)
                    U.Add(o);
            }
        }

        foreach(var a in U)
        {
            int D = SkillData.UserTarget.UnitData.LastAtk;
            var H = DiceSystem.HitDice(SkillData.UserTarget, a);
            CardGameManager.Instance.GameNotifyAction_Net.Cmd_UnitAtkUnit_ServerRpc(D, SkillData.UserTarget.UnitID, a.UnitID, H, AttackDamgerTpye.Normal);
         //   a.UnitData.TakeDamger(SkillData.UserTarget.UnitData.LastAtk, SkillData.UserTarget.UnitData);
        }
        
    }

    public override List<MapSolt> GetMaps()
    {
        return CardGameManager.Instance.GetUserEnemyMapArea(SkillData.UserTarget).GetAllMapSolt();
    }

    public override void UIShow(BattleActionPool pool)
    {
        pool.AttackUnitIcon.sprite = SkillData.UserTarget.UnitData.cardArt;
        pool.GUIText.text = "全體攻擊";
        pool.DamgerUnitIcon.sprite = null;
    }
}
