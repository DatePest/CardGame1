using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck_HeroEdit_Position : MonoBehaviour
{
    [SerializeField] Deck_HeroEditBox Obj;
    [SerializeField] Transform Target;
    [SerializeField] Deck_HeroItem HeroItem;
    List<Deck_HeroEditBox> HeroEditBoxs=new();

    public Deck_HeroItem[] HeroDatas { get; private set; } = new Deck_HeroItem[3];
    private void Awake()
    {
        SetBox(9);
        SetHeroItem(HeroDatas.Length);
    }
    public void LoadDataPostint(DeckData data)
    {
        foreach(var a in HeroDatas)
        {
            a.RemoveCurrnetBox();
        }
        for (int a = 0; a < data.Heros.Length; a++)
        {
            if (data.Heros[a].UnitUid == "") continue;
            foreach (var b in HeroEditBoxs)
            {
                if(b.Position == data.Heros[a].MapPoint)
                {
                    b.SetHeroItem(HeroDatas[a]);
                    break;
                }
            }
        }
    }


    public void SetHeroItem(int i)
    {
        for (int a = 0; a < i; a++)
        {
            var g = Instantiate(HeroItem, transform);
            g.gameObject.SetActive(false);
            HeroDatas[a] = g;
        }
    }

    public void SetBox(int i)
    {
        for (int a = 0; a < i; a++)
        {
            var g = Instantiate(Obj, Target);
            g.SetPosition(a);
            HeroEditBoxs.Add(g);
        }
    }
    
    public void EnbleHeroItem(int Number ,string Uid ,Sprite Icon)
    {
        ChangeNameHeroItem(Number, Uid, Icon);
        foreach (var a in HeroEditBoxs)
        {
            if(a.HeroItem == null)
            {
                a.SetHeroItem(HeroDatas[Number]);
                break;
            }
        }
    }
    public void DisenbleHeroItem(int Number)
    {
        foreach (var a in HeroEditBoxs)
        {
            if (a.HeroItem != null&& a.HeroItem.Uid == HeroDatas[Number].Uid)
            {
                a.RemovetHeroItem();
                break;
            }
        }
        ChangeNameHeroItem(Number, null);
    }
    public void ChangeNameHeroItem(int Number, string Uid, Sprite Icon = null)
    {
        HeroDatas[Number].Uid = Uid;
        HeroDatas[Number].image.sprite = Icon;
    }


}
