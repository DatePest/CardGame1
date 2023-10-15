using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapScript : MonoBehaviour
{
    public PlayerOBJ UserPlayer;
    public SO_Unit TestUnit;
    public MapSolt[] mapSolts;
    [SerializeField] UnitSolt unitSolt;
    // Start is called before the first frame update
    private void Awake()
    {
         mapSolts = GetComponentsInChildren<MapSolt>();
    }
    void Start()
    {
        int i = Random.Range(0, mapSolts.Length);
       // mapSolts[i].Add_Set_Solt((unitSolt));
        //var T = Instantiate(TestUnit);
       // mapSolts[i].unitSolt.SetUnit(TestUnit);


    }
}
