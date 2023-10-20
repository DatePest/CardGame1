using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;

[CreateAssetMenu(fileName = "ToSearchCard", menuName = "SO/CardSkill/ToSearchFindCard")]
public sealed class Skill_ToSearchCard : Skill_NotTarget_WaitSelectCard
{
    [SerializeField] bool IsUseDisCardAbilities = false;
    [Header("是否啟用額外的判斷技能")]
    [SerializeField] bool EX_Enable =false;
    [SerializeField] List<SO_CardCheckType> CardChecks;
    [SerializeField] List<Skill_NotTarget> TurnSkills, FalseSkills;
    [SerializeField] List<Skill_NeedTargetBase> TurnSkills2, FalseSkills2;


    Skill_ToSearchCard()
    {
        skill_Rule = Enum_Skill_Rule.Draw;
    }

    public override async Task UseSkill(AbilityNeedData data)
    {
        if (data.SelectCardUids == null|| data.SelectCardUids.Length <1)
        {
            return;
        }
        var Cards = await data.CurrentUsePlayer.cardSpawnScript.FindCardGoto(data.SelectCardUids, From, Target);
       
        if (From2 != CardsPileEnum.Null)
        {
            //var C = data.CurrentUsePlayer.cardSpawnScript.FindCardSAll(data.SelectCardUids);
            List<int> Cs = new();
            foreach (var a in data.SelectCardUids)
            {
                Cs.Add(a);
            }
            foreach (var a in Cards)
            {
                var aC =a.GetComponentInChildren<CardSolt>();
                Cs.Remove(aC.CardUid);
            }

            var Cards2 = await data.CurrentUsePlayer.cardSpawnScript.FindCardGoto(Cs.ToArray(), From2, Target);
            Cards.AddRange(Cards2);
        }
        
        while (!data.CurrentUsePlayer.cardSpawnScript.SpawnEnd)
        {
            await Task.Delay(300);
        }

        //ebug.Log("UseSkill");
        if (EX_Enable)
        {
            bool T = true;
            foreach(var a in Cards)
            {
                T = true;
                foreach (var b in CardChecks)
                {
                    if (b.CheckCard(a) == false)
                    {
                        T = false;
                        break;
                    }
                }
                if (T == true)
                    break;
            }

            if (T)
            {
                foreach(var a in TurnSkills)
                {
                    await a.UseSkill(data);
                }
                foreach (var a in TurnSkills2)
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
                foreach (var a in FalseSkills2)
                {
                    await a.UseSkill(data);
                }
            }
        }

        if (From == CardsPileEnum.hand && Target == CardsPileEnum.disdeck)
        {
            //CardsPileEnum From, Target;
            data.CurrentUsePlayer.UseSkillDisCardEvent(Cards.Count);
        }

        if (IsUseDisCardAbilities)
        {
            if (NetworkManager.Singleton.LocalClientId != data.CurrentUsePlayer.OwnerClientId) return;
            await WaitSelectCard.UseDisCardAbilities(Cards);
        }
        
    }
}
