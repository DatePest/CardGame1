using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class Deck_HeroEdit_Dropdown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown[] dropdowns;
    [SerializeField] CardDeckCustom_Deck custom_Deck;
    [SerializeField] CardDeckCustom_CardSelect cardDeckCustom_Card;
    [SerializeField] UnitSolt unitSolt;
    public TMP_Dropdown[] Dropdowns => dropdowns;
    Deck_HeroEdit_Position _HeroEdit_Position;
    //DeckData DeckData = new();
    Dictionary<TMP_Dropdown, int> dropdownskey =new();
    List<CardDeckCustom_UnitData> Units = new();
    string[] SelectHeros = new string[3];

    private void Awake()
    {
        _HeroEdit_Position = GetComponent<Deck_HeroEdit_Position>();
        custom_Deck = FindFirstObjectByType<CardDeckCustom_Deck>();
    }
    class CardDeckCustom_UnitData
    {
        public string UnitName, Uid;
        public int DropdownID = -1;
        public CardDeckCustom_UnitData(string s1 , string s2,int I)
        {
            UnitName = s1;
            Uid = s2;
            DropdownID = I;
        }
    }
    private void Start()
    {
        int i = 0;
        foreach (var b in dropdowns)
        {
            dropdownskey.Add(b, i);
            b.options.Clear();
            b.options.Add(new TMP_Dropdown.OptionData() { text = "Select" });
            i++;
        }
        i = 1;
        foreach (var a in GameManager.Instance.DataBase.Units)
        {
            if (a.UnitTpye != UnitTpye.Hero) continue;
            Units.Add(new(a.UnitName, a.UintID,i ));
            //Debug.Log(a.UnitName);
            foreach (var b in dropdowns)
            {
                b.options.Add(new TMP_Dropdown.OptionData() { text = a.UnitName});
            }
            i++;
        }
        foreach (var b in dropdowns)
        {
            b.onValueChanged.AddListener((a)=> { DropdownEvent(b); });
        }

       
    }


    public void ShowUnit(string Uid)
    {
        if (unitSolt.gameObject.activeSelf == false) unitSolt.gameObject.SetActive(true);
        var T =GameManager.Instance.DataBase.StringToUnit(Uid, false);
        unitSolt.DeckCustom_DisplayUnit(T);
    }
    void DropdownEvent(TMP_Dropdown dropdown)
    {
        var Tarobj = dropdown.GetComponent<TargetObj>().obj;
        CardDeckCustom_ShowUnit _ShowUnit = Tarobj.GetComponent<CardDeckCustom_ShowUnit>();
        if (_ShowUnit == null) _ShowUnit = Tarobj.AddComponent<CardDeckCustom_ShowUnit>();
        //var S = dropdown.options[dropdown.value].text;
        if (dropdown.value == 0)
        {
            Tarobj.GetComponent<Image>().sprite = null;
            _HeroEdit_Position.DisenbleHeroItem(dropdownskey[dropdown]);
            SelectHeros[dropdownskey[dropdown]] = null;
            cardDeckCustom_Card.UpdateShow(SelectHeros);
            return;
        }
        foreach (var b in dropdowns)
        {
            if (b == dropdown)
                    continue;
            if (b.value == 0)
                    continue;
            if(dropdown.value == b.value)
                {
                    Debug.Log("ADD Clear");
                    b.value = 0;
                    b.GetComponentInChildren<Image>().sprite = null;
                }
        }
        var T = Units.Where(x => x.DropdownID == dropdown.value).First();
        SelectHeros[dropdownskey[dropdown]] = T.Uid;
        cardDeckCustom_Card.UpdateShow(SelectHeros);
        custom_Deck.ReCheck(SelectHeros);
        var sp = GameManager.Instance.DataBase.StringToIconSpriteFormHero(T.Uid);
        _ShowUnit.SetImage(sp);
        _ShowUnit.SetUid(T.Uid);
        _HeroEdit_Position.EnbleHeroItem(dropdownskey[dropdown],T.Uid , sp);
        ShowUnit(T.Uid);


    }
    public void DataLoad(DeckData data)
    {
        for (int a = 0; a < data.Heros.Length; a++)
        {
            if (data.Heros[a].UnitUid == null|| data.Heros[a].UnitUid == "")
            {
                //dropdowns[a].value = 0;
                continue;
            }
            var T = Units.Where(x => (x.Uid == data.Heros[a].UnitUid)).First();
            dropdowns[a].value = T.DropdownID;
        }
    }
    public bool CheckHaveHero(string Uid)
    {
        if (Uid == null|| Uid == "") return true;
        //Debug.Log(hero);
        var Name = Units.Where(x => (x.Uid == Uid)).First();

        foreach (var a in dropdowns)
        {
            if (a.value == 0) continue;
            if (Name.Uid == Uid)
            {
                return true;
            }
        }
        return false;
    }

}
