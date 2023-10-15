using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class MouseManager : MonoBehaviour
{
    public GameObject CurrnetSelect;
    public Camera M_camera;
    public RaycastHit[]  CurrnetHits = null;
    MapShowTpye mapShow = MapShowTpye.Null;
    public MapShowTpye MapShow => mapShow;
    List<MapSolt> CurrentMaps = new();

    private void Start()
    {
        M_camera = GetComponentInChildren<Camera>();
    }
    public void SetMapShowTpye(MapShowTpye tpye)
    {
        mapShow = tpye;
    }
    public void EnterMapShow(MapSolt mapSolt)
    {
        //Debug.Log(mapShow);

        CurrentMaps = mapSolt.MyMapArea.Get_SwitchMaps(mapSolt, mapShow);
        foreach (var a in CurrentMaps)
        {
            a.GetComponent<Ground_IEvent>().SetColor(Color.red);
        }
    }
    public void ExitMapShow()
    {
        if (CurrentMaps.Count == 0) return;
        foreach (var a in CurrentMaps)
        {
            a.GetComponent<Ground_IEvent>().SetColor_Orignal();
        }
        CurrentMaps.Clear();
    }

    public void MouseCurrnetSelect(GameObject eventData)
    {
        CurrnetSelect = eventData;
    }
    public void SetCurrentMaps(List<MapSolt> maps)
    {
        ExitMapShow();
        CurrentMaps = maps;
        foreach (var a in CurrentMaps)
        {
            a.GetComponent<Ground_IEvent>().SetColor(Color.red);
        }
    }

    //public async void CurrnetSelect_return_SO_Unit( out SO_Unit unit)
    //{
    //    //SO_Unit U =null;
    //    unit = await MouseCurrnetSelectTTTTTTT();
    //}
    //public async Task<SO_Unit> MouseCurrnetSelectTTTTTTT()
    //{
    //    SO_Unit U = null; 

    //    while (U == null)
    //    {
    //        if (CurrnetHits != null)
    //            foreach (var result in CurrnetHits)
    //            {
    //                if (result.transform.TryGetComponent<Ground_IEvent>(out var T))
    //                {

    //                    if (T.GetUnit() != null)
    //                    {
    //                        U = T.GetUnit();
    //                    }
    //                    break;
    //                }
    //                continue;
    //            }

    //    }

    //    return U;
    //}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurrnetHits = null;
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = M_camera.ScreenPointToRay(mousePosition);
            //Debug.Log($"X+{Input.mousePosition.x}  ,, Y+{Input.mousePosition.y}");
            CurrnetHits = Physics.RaycastAll(ray);
            //Debug.Log($"CurrnetHits {CurrnetHits.Length}");
        }

    }
}
public enum MapShowTpye: byte
{
    Null,
    X_Tpye,
    Row_Tpye,
    Column_Tpye,
    Cross_Tpye,
    GetExpansion_3,
    All
}
 