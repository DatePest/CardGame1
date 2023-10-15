using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_EndToRemove", menuName = "SO/CardSkill/EndToRemove")]
public sealed class Skill_EndToRemove : Skill_NotTarget
{
    Skill_EndToRemove()
    {
        skill_Rule = Enum_Skill_Rule.Null;
    }
    public override async Task UseSkill(AbilityNeedData data)
    {
        var card = data.CurrentUsePlayer.cardSpawnScript.FindCardsPile(CardsPileEnum.hand).Cards;

        foreach(var a in card)
        {
            var so = a.GetComponentInChildren<CardSolt>();
            if(so.CardUseingLook == true)
            {
                so.SetEndToRomve(true);
                return;
            }
           
        }
        
    }

}
