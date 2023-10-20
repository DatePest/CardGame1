using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataBese : MonoBehaviour
{
    public SO_CardAbilityFrame CardAbilityFrame { get; private set; }
    public List<SO_CardBase> Cards { get; private set; }
    public List<SO_SKillAbility> SKills { get; private set; }
    public List<SO_Unit> Units { get; private set; }
    public List<BuffBase> Buffs { get; private set; }
    public List<AbilityBase> Abilitys { get; private set; }
    public Dictionary<string, SO_Unit> HeroUIDs { get; private set; } = new();

    private void Awake()
    {
        Cards = Resources.LoadAll<SO_CardBase>("Card").ToList();
        SKills = Resources.LoadAll<SO_SKillAbility>("CardSkill").ToList();
        Units = Resources.LoadAll<SO_Unit>("Unit").ToList();
        CardAbilityFrame = Resources.Load<SO_CardAbilityFrame>("SO_CardAbilityFrame");
        Buffs = Resources.LoadAll<BuffBase>("Buff").ToList();
        Abilitys = Resources.LoadAll<AbilityBase>("Ability").ToList();
        foreach(var a in Units)
        {
            HeroUIDs.Add(a.UintID, a);
        }
    }

    public AbilityBase StringToAbilityBase(string S)
    {
        try
        {
            var C = Abilitys.Where(n => n.AbilityId == S).SingleOrDefault();
            return Instantiate(C);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return null;
    }
    public Sprite StringToIconSpriteFormHero(string S)
    {
        try
        {
            var C = Units.Where(n => n.UintID == S).SingleOrDefault();
            return C.cardArt;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return null;
    }

    public BuffBase StringToBuff(string S ) 
    {
        try
        {
            var C = Buffs.Where(n => n.BuffID == S).SingleOrDefault();
            return C.DeepCopy();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return null;
    }


    public SO_CardBase StringToCard(string S)
    {
        try
        {
            var C = (SO_CardBase)Cards.Where(n => n.CardId == S).SingleOrDefault();
            return C.DeepCopy();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("StringToCard___"+S);
        }
        return null;
    }
    public SO_Unit StringToUnit(string S,bool Deep =true)
    {
        try
        {
            var C = (SO_Unit)Units.Where(n => n.UintID == S).SingleOrDefault();
            if (Deep)
                return C.DeepCopy();
            else
                return C;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Debug.LogError("StringToUnit___" + S); 
        }
        return null;
    }


    public List<SO_CardBase> DeckStringToDeckCard(List<string> ListS)
    {
        List<SO_CardBase> DeckCard = new();
        foreach( var S in ListS)
        {
            try
            {
                var C = (SO_CardBase)Cards.Where(n => n.CardId == S).SingleOrDefault();
                var T = C.DeepCopy();

                DeckCard.Add(T);
            }
            catch(Exception e)
            {
                Debug.LogError(e);  
            }
             
        }

        return DeckCard;
    }
    public List<string> DeckDeckCardToString(List<SO_CardBase> ListS)
    {
        List<string> DeckCard = new();
        foreach (var S in ListS)
        {
            try
            {
                //var C = (string)Cards.Where(n => n.cardname == S).SingleOrDefault();
                var T = S.CardId;

                DeckCard.Add(T);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
        return DeckCard;
    }




}
