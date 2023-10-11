using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDeckCustom_CardSelect : MonoBehaviour
{
    [SerializeField] ObjPool pool;
    [SerializeField] CardDeckCustom_CardDisplay ShowCardOBJ;
    //[SerializeField] TextMeshProUGUI Text;
    public List<CardDeckCustom_CardDisplay> AllList { get; private set; } = new();
    //public List<CardDeckCustom_CardCheckBase> cardChecks = new();

    void Start()
    {
       
        if (GameManager.Instance == null) return;
        foreach (var a in GameManager.Instance.DataBase.Cards)
        {
            if (a.CardId == "C000") continue;//TestCard
            pool.GetPool(out var p);
            //p.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var C = p.GetComponentInChildren<CardDeckCustom_CardDisplay>();
            C.SetCard(a);
            AllList.Add(C);
            C.AddButtonTrigger(Button);
        }
    }
    void Button(CardDeckCustom_CardDisplay Obj)
    {
        ShowCardOBJ.SetCard(Obj.CardSO);
    }

    public void UpdateShow(string[] Heros)
    {
        //if (cardChecks.Count < 1)
        //{
        //    foreach (var a in SelectList)
        //    {
        //        a.gameObject.SetActive(true);
        //    }
        //}
        foreach (var a in AllList)
        {
            a.gameObject.SetActive(false);
        }

        if(Heros.All(x => x == null))
        {
            foreach (var a in AllList)
            {
                a.gameObject.SetActive(true);
            }
            return;
        }

        //var T = AllList.SelectMany(x => Heros, (x, y) => new { All = x, Id = y }).Where(a => a.All.CardSO.HeroID == a.Id).ToList();
        var T =
        from a in AllList
            //from k in Heros
        where Heros.Any(x => a.CardSO.HeroID == x)|| a.CardSO.HeroID== ""
        select a;
        foreach (var a in T)
        {
            a.gameObject.SetActive(true);
        }

    }
}
