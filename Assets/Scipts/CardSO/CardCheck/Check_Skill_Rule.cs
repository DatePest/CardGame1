using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Check_Skill_Rule", menuName = "SO/CardCheck/Skill_Rule")]
public class Check_Skill_Rule : SO_CardCheckType
{
    [SerializeField] Enum_Skill_Rule Skill_Rule;
    public override bool CheckCard(GameObject GList)
    {
        var so = GList.GetComponentInParent<CardSolt>().CardSO;
        foreach (var a in so.Abilities_1)
        {
            if (a.Skill_Rule == Skill_Rule)
            {
                return F.ReNox(true);
            }
        }
        if (so.EX1_Wex.Count > 0)
        {
            foreach (var a in so.EX1_Wex)
            {
                if (a.Skill_Rule == Skill_Rule)
                {
                    return F.ReNox(true);
                }
            }
        }
        if (so.EX2_Dex.Count > 0)
        {
            foreach (var a in so.EX2_Dex)
            {
                if (a.Skill_Rule == Skill_Rule)
                {
                    return F.ReNox(true);
                }
            }
        }
        return F.ReNox(false);
    }
    public override List<GameObject> SearchCheckCard(List<GameObject> GList)
    {
        List<GameObject> NewList = new();
        foreach (var c in GList)
        {
            var so = c.GetComponentInParent<CardSolt>().CardSO;
            foreach (var a in so.Abilities_1)
            {
                if (a.Skill_Rule == Skill_Rule)
                {
                    NewList.Add(c);
                    continue;
                }
            }
            if (so.EX1_Wex.Count > 0)
            {
                foreach (var a in so.EX1_Wex)
                {
                    if (a.Skill_Rule == Skill_Rule)
                    {
                        NewList.Add(c);
                        continue;
                    }
                }
            }
            if (so.EX2_Dex.Count > 0)
            {
                foreach (var a in so.EX2_Dex)
                {
                    if (a.Skill_Rule == Skill_Rule)
                    {
                        NewList.Add(c);
                        continue;
                    }
                }
            }
        }
        return NewList;
    }
}
