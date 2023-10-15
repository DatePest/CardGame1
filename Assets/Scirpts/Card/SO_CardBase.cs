using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "SO/New SO_Card")]
public class SO_CardBase : ScriptableObject
{
    public Sprite cardArt;
    public string cardname;
    [SerializeField]
    string cardId;
    public string CardId { get { return cardId; } }
    public CardType cardType;
    public string HeroID;
    public CardAttributesType CardAttributesType;
    public List<SO_SKillAbility> Abilities_1, EX1_Wex,EX2_Dex , DiscardAbilities ;
    [TextArea(4,8)]
    public string SkillDepiction;

    [Header("ExCost")]
    public int EX1_Wex_Cost, EX2_Dex_Cost;
    [Header("卡片數量上限")]
    public int DeckUpperlimit =2;

    public bool CanEx1Check()
    {
        if(EX1_Wex.Count < 1) return false;

        if (10 - CardGameManager.Instance.Ts_Manager.TsTimes < EX1_Wex_Cost ) return false;

        return true;
    }
    public bool CanEx2Check()
    {
        if (EX2_Dex.Count < 1) return false;
        if ( CardGameManager.Instance.Ts_Manager.TsTimes < EX2_Dex_Cost) return false;

        return true;
    }


    public SO_CardBase DeepCopy()
    {
        var C = Instantiate(this);
        C.Abilities_1.Clear();
        foreach(var a in Abilities_1)
        {
            C.Abilities_1.Add(Instantiate(a));
        }
        C.EX1_Wex.Clear();
        foreach (var a in EX1_Wex)
        {
            C.EX1_Wex.Add(Instantiate(a));
        }
        C.EX2_Dex.Clear();
        foreach (var a in EX2_Dex)
        {
            C.EX2_Dex.Add(Instantiate(a));
        }
        C.DiscardAbilities.Clear();
        foreach (var a in DiscardAbilities)
        {
            C.DiscardAbilities.Add(Instantiate(a));
        }
        return C;
    }
}


/// <summary>
/// 卡片的技能組裡面可能有一些額外技能效果 但是格式問題 卡片的敘述目前是參照第一個的的技能 
/// 比如說先攻擊 然後抽一張卡 :
/// 考量到這部分問題  因此目前 這些複合技能都頭都需要專門開一個技能說明 也就是第一個開頭的技能要單獨建立
/// </summary>

public enum CardType
{
    Attack,Def,Buff,Ts,Action
}
//public enum CardHeroType
//{
//    Null,Hero001,H002, H003, H004, H005, H006, H007, H008, H009, H010, H011, H012
//}

public enum CardAttributesType
{
    Cold, Dark, Fire, Land,Light,Sky,Wex,Dex

}

