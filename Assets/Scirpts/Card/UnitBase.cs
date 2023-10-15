using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ŽbŽžœEŠü
///ˆÈœEŠü
/// </summary>
public abstract class UnitBase
{
    public string UnitName;
    public SO_Unit UnitSO { get; private set; }
    public int ModfirAtk { get; private set; }
    public int ModfirArrmor { get; private set; }
    //public int ModfirHP { get; private set; }
    public int LastAtk { get { return UnitSO.UnitAtk + ModfirAtk; } }
    public int LastArrmor { get { return UnitSO.UnitArmor + ModfirArrmor; }  }
    //public int LastHP { get { return UnitSO.cardNowHp; } set { UnitSO.cardNowHp = value; } }
    // Equipment solt
    //public List<Buff> MyBuff;

   
}


