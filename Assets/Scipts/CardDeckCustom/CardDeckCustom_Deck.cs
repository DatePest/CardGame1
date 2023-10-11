using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDeckCustom_Deck : MonoBehaviour
{
    [SerializeField] ObjPool objPool;
    [SerializeField] CardDeckCustom_CardDisplay CurrentCard;
    [SerializeField] Deck_HeroEdit_Dropdown HeroEdit_Dropdown;


    public void LoadDataToDeck(DeckData data)
    {
        objPool.RemoveAll();
        foreach(var a in data.Dards)
        {
            //Debug.Log(a);
            if (a == null) continue;
            objPool.GetPool(out var G);
             G.GetComponentInChildren<CardDeckCustom_CardDisplay>().SetCard(GameManager.Instance.DataBase.StringToCard(a));
        }
        
    }
    private void Start()
    {
        foreach(var a in objPool.DisenableList)
        {
            a.transform.localScale =new Vector3(0.4f, 0.4f, 0.4f);
            a.GetComponentInChildren<CardDeckCustom_CardDisplay>().AddButtonTrigger(Button);
        }
        
        CurrentCard.gameObject.SetActive(false);
        //CurrentCard.AddButtonTrigger(Button);
    }
    public string[] ReturnCards()
    {
        List<string> SList = new();
        foreach (var a in objPool.EnableList)
        {
            SList.Add(a.GetComponentInChildren<CardDeckCustom_CardDisplay>().CardSO.CardId);
        }
        return SList.ToArray();
    }

    private void Button(CardDeckCustom_CardDisplay obj)
    {
        if (CurrentCard == null|| obj.CardSO ==null) return;
        CurrentCard.SetCard(obj.CardSO);
    }

    public void JoinDeck()//use
    {
        if (CurrentCard.CardSO == null) return;
        if (objPool.EnableList.Count >= 30) return;
        bool B;
        if (CurrentCard.CardSO.HeroID != null)
        {
            B = HeroEdit_Dropdown.CheckHaveHero(CurrentCard.CardSO.HeroID);
        }
        else
            B = true;

        if (B == true)
        {
            var i = CheckUpperlimit(CurrentCard.CardSO.DeckUpperlimit, CurrentCard.CardSO.CardId);
            if (i == false) return;

            objPool.GetPool(out GameObject g);
            var c = g.GetComponentInChildren<CardDeckCustom_CardDisplay>();
            c.SetCard(CurrentCard.CardSO);
        }
    }
    public bool CheckUpperlimit(int Times, string CardId)
    {
        int i = 0;
        foreach (var a in objPool.EnableList)
        {
            var c = a.GetComponentInChildren<CardDeckCustom_CardDisplay>();
            if (c.CardSO.CardId == CardId)
            {
                i++;
            }
        }
        if(i < Times)
        {
            return true;
        }
        return false;
    }

    public void RemoveDeck() //use
    {
        GameObject target =null;
        foreach (var a in objPool.EnableList)
        {
            var c = a.GetComponentInChildren<CardDeckCustom_CardDisplay>();
            if(c.CardSO.CardId == CurrentCard.CardSO.CardId)
            {
                target = a;
                break;
            }
        }
        if(target!= null)
        {
            objPool.RemovePool(target);
        }
    }

    public void ReCheck(string[] Id)
    {
        var T =
            from a in objPool.EnableList
            where Id.All(x => x != a.GetComponentInChildren<CardDeckCustom_CardDisplay>().CardSO.HeroID) && a.GetComponentInChildren<CardDeckCustom_CardDisplay>().CardSO.HeroID != ""
            select a;
        List<GameObject> GL = new(T);

        foreach (var a in GL)
        {
            objPool.RemovePool(a);
        }


        //CardDeckCustom_CardDisplay c;
        //bool B;
        //List<GameObject> GL = new();
        //foreach (var a in objPool.EnableList)
        //{
        //     c = a.GetComponentInChildren<CardDeckCustom_CardDisplay>();
        //    //   Debug.Log(c.CardSO.HeroID + "id" + Id);
        //    B = true;
        //    foreach (var b in Id)
        //    {
        //        if (c.CardSO.HeroID == b)
        //        {
        //            B = false;
        //            break;
        //        }
        //    }
        //    if (B== true)
        //        GL.Add(a);
        //}
        //foreach(var a in GL)
        //{
        //    objPool.RemovePool(a);
        //}
        //var T = objPool.EnableList.SelectMany(x => Id, (x, id) => new { pool = x, IDs = id }).
        //    Where(a => a.pool.GetComponentInChildren<CardDeckCustom_CardDisplay>().CardSO.HeroID == a.IDs).ToList();
        //foreach (var a in T)
        //{
        //    objPool.EnableList.RemoveAll(x => x == a.pool);
        //}
    }
}
