using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MapSolt : NetworkBehaviour
{
    public UnitSolt Unitsolt { get; private set; }
    public MapArea MyMapArea { get; private set; }
    public ulong PlayerOwnerNumberID { get; private set; }
    public int MapSoltID { get; private set; }
    private void Awake()
    {
        Unitsolt = GetComponentInChildren<UnitSolt>();
    }
    public void SetUnit(Unit U)
    {
        Unitsolt.SetUnit(U);
        U.SetCurrentMapSolt(this);
    }

    public void RemoveUnit()
    {
        if (Unitsolt == null) return;
        Unitsolt.ReMoveUnit();
    }

    public void SetPlayerOwnerNumberID(ulong ID)
    {
        PlayerOwnerNumberID = ID;
        if(PlayerOwnerNumberID != NetworkManager.Singleton.LocalClientId)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }
    public void SetMyMapArea(MapArea mapArea , int i)
    {
        MyMapArea = mapArea;
        MapSoltID = i;
    }
    public Unit GetUnit(bool UseOut=false)
    {
        if (Unitsolt.My_Unit != null)
            if (UseOut)
                return GetComponentInChildren<Unit>();
            else
                return Unitsolt.My_Unit;

        return null;
    }

}
