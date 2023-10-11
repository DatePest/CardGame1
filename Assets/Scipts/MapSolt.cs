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
    //public void Add_Set_Solt(UnitSolt unit)
    //{
    //    if(unitSolt==null)
    //    {
    //        var T = Instantiate(unit);
    //        T.transform.SetParent(transform);
    //        T.transform.localPosition = Vector3.zero;
    //        Vector3 v = new Vector3(0, -90, 90);
    //        T.transform.localRotation = Quaternion.identity;
    //        T.transform.localRotation = Quaternion.Euler(v);
    //        unitSolt = T;
    //    }

    //}

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
    [ServerRpc]
    public void Net_unitSolt_RemoveUnitServerRpc()  => Net_unitSolt_RemoveUnitClientRpc();
    [ClientRpc]
     void Net_unitSolt_RemoveUnitClientRpc() => RemoveUnit();



    //[ServerRpc]
    //public void Net_unitSolt_SetUnitServerRpc(NetworkBehaviourReference target)
    //{
    //    target.TryGet(out Unit networkBehaviour);
    //    if (networkBehaviour.TryGetComponent<NetworkObject>(out NetworkObject N))
    //        N.TrySetParent(transform, false);
    //    unitSolt.SetUnit(networkBehaviour);
    //    Net_unitSolt_SetUnitClientRpc(target);
    //}
    //[ClientRpc]
    // void Net_unitSolt_SetUnitClientRpc(NetworkBehaviourReference target)
    //{
    //    if (IsHost) return;
    //    target.TryGet(out Unit networkBehaviour);
    //    unitSolt.SetUnit(networkBehaviour);
       
    //}
}
