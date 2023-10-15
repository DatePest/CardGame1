using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardsPileEnumToText
{
    public static string ToText(CardsPileEnum pileEnum)
    {
        switch (pileEnum)
        {
            case CardsPileEnum.hand:
                return "手牌";
            case CardsPileEnum.deck:
                return "牌組";
            case CardsPileEnum.Remove:
                return "除外";
            case CardsPileEnum.disdeck:
                return "丟棄";
        }
        throw new NotImplementedException("CardsPileEnum is Null");

        return null;
    }

}
public enum CardsPileEnum : byte
{
    hand, deck, disdeck, search, Remove, Null
}