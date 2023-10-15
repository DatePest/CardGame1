using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New SO_CardAbilityFrame", menuName = "SO/SO_CardAbilityFrame")]
public class SO_CardAbilityFrame : ScriptableObject
{
    [SerializeField]
    Sprite[] cardAbilityFrames ;
    [SerializeField]
    Sprite[] cardAbilityIcons;

    public Sprite[] CardAbilityFrames => cardAbilityFrames;
    //public enum HeryTypeAttributes
    //{
    //    Sky, Earth, Fire, Water, Light, Dark
    //}

    public Sprite GetCardAbilityFrame(CardAttributesType typeAttributes) 
    {
        
        var A = ((int)typeAttributes);
        return cardAbilityFrames[A];
    }
    public Sprite GetcardAbilityIcons(CardAttributesType typeAttributes)
    {

        var A = ((int)typeAttributes);
        return cardAbilityIcons[A];

    }
}
