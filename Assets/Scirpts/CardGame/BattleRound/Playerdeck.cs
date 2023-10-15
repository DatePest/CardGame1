using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdeck :MonoBehaviour
{
    //public Playerdeck(int i)
    //{
    //    Playerid = i;
    //}
    //public int Playerid;
    public List<string> mydeck { get { return GameManager.Instance.DataBase.DeckDeckCardToString(mySOdeck); } }

    public List<SO_CardBase> mySOdeck;
    public List<SO_CardBase> TESTmySOdeck  { get { return GameManager.Instance.DataBase.DeckStringToDeckCard(mydeck); } } // use

}
