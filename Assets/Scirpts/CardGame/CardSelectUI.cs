using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSelectUI : MonoBehaviour
{

    [SerializeField] ObjPool pool_1, pool_2;
    [SerializeField] GameObject ChildrenDisplay_1, ChildrenDisplay_2;
    [SerializeField] TextMeshProUGUI Text_1, Text_2;
    public List<int> SelectList { get; private set; } = new();
    int[] SelectArray;
    int NeedSize = 0 , CurrntTag;
    

    private void Awake()
    {
        ChildrenDisplay_1.SetActive(false);
        ChildrenDisplay_2.SetActive(false);
    }
    public bool IsDisplay()
    {
        return ChildrenDisplay_1.activeSelf;
    }

    public void DisplayStart_1(List<GameObject> GList , int Size)
    {
        SelectList.Clear();
        ChildrenDisplay_1.SetActive(true);
        NeedSize = Size;
        foreach (var a in GList)
        {
            pool_1.GetPool(out var p);
            p.GetComponentInChildren<CardDisplay_CardSelect>().SetCard(a);
        }
        Text_1.text = ($"You need to select {NeedSize} more");//You need to select N more
    }
    public void SelectReturn_1(int G,GameObject ObjPool)
    {
        SelectList.Add(G);
        pool_1.RemovePool(ObjPool);

        Text_1.text = ($"You need to select {NeedSize- SelectList.Count} more");
        if (SelectList.Count < NeedSize) return;
        pool_1.RemoveAll();
        ChildrenDisplay_1.SetActive(false);
        NeedSize = 0;;
    }
    public void DisplayStart_2(List<GameObject> GList, int Size)
    {
        SelectList.Clear();
        ChildrenDisplay_2.SetActive(true);
        NeedSize = Size;
        SelectArray = new int[Size];
        CurrntTag = 0;
        foreach (var a in GList)
        {
            pool_2.GetPool(out var p);
            p.GetComponentInChildren<CardDisplay_TopreturnSelect>().SetCard(a);
        }
        Text_2.text = ($"Current is {CurrntTag}");
    }
    public void SelectReturn_2(int CardUid, CardDisplay_TopreturnSelect Obj , int TagN)
    {

        if(TagN == -1)
        {
            SelectArray[CurrntTag] = CardUid;
            Obj.SetuGUI(CurrntTag + 1);
            for (int i = 0; i < SelectArray.Length; i++)
            {
                if (SelectArray[i] == 0)
                {
                    CurrntTag = i;
                    break;
                }
            }
        }
        else
        {
            SelectArray[TagN-1] = 0; 
            for (int i = 0; i < SelectArray.Length; i++)
            {
                if (SelectArray[i] == 0) // 0== null
                {
                    CurrntTag = i;
                    break;
                }
            }
            Obj.Close();
        }
        
        Text_2.text = ($"Current is {CurrntTag +1}");
        if (SelectArray[SelectArray.Length - 1] == 0) return;
        NeedSize = 0;
        SelectList.Clear();
        for (int i=0;i< SelectArray.Length; i++)
        {
            SelectList.Add(SelectArray[i]);
        }
        pool_2.RemoveAll();
        ChildrenDisplay_2.SetActive(false);
    }
}
