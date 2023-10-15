using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "IntAbilityValue", menuName = "SO/IntAbilityValue")]
public abstract class IntAbilityValue_SO : ScriptableObject
{
    public abstract int GetValue(AbilityNeedData data);

}