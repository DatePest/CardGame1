using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;

public class RoomLoadDeck : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] LoadScreen loadScreen;
    DeckData deckData =null;
    public bool DeckCheck { get { if (deckData != null) return true; else return false; } }
    string extension => SaveSettion.extension;
    string SavePath => SaveSettion.SavePath;
    private void Awake()
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }
    }
    //testdeck
    DeckData Test()
    {
        var Deck = new DeckData();
        for (int a = 0; a < Deck.Dards.Length; a++)
        {
            Deck.Dards[a] = $"C00{Random.Range(00, 4)}";
            //Deck.Dards[a] = $"C000";
        }
        var T = Random.Range(1, 5);
        var P = Random.Range(1, 3);
        Deck.Heros[0].UnitUid = "Hero0" + "01";
        Deck.Heros[1].UnitUid = "Hero0" + "07";
        Deck.Heros[2].UnitUid = "Hero0" + "12";
        //for (int a = 0; a < Deck.Heros.Length; a++)
        //{
        //    Deck.Heros[a].HeroName = "Hero00" + 0*(a + 7);
        //    Deck.Heros[a].MapPoint =P+(a * 3);
        //}
        return Deck;
    }
    public void ShowLoadScreen()
    {
        loadScreen.gameObject.SetActive(true);

        var Names = Directory.GetFiles(SavePath);
        loadScreen.Show(Names, LoadDeckData, extension);
    }
     void LoadDeckData(string path)
    {
        loadScreen.gameObject.SetActive(false);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            var stream = new FileStream(path, FileMode.Open);
            var data = formatter.Deserialize(stream) as DeckData;

            deckData = data;
            stream.Close();
            Text.text = Path.GetFileNameWithoutExtension(path);
            var b =CheckDeckData(deckData);
            if (b)
            {
                Text.text = "The  cannot be used";
                deckData = null;
            }

        }
        else
        {
            Debug.LogError("Save file not found in" + path);
        }
    }
    public DeckData GetDeck()
    {
        if (deckData == null) return null;

        return deckData;
    }
    public bool CheckDeckData(DeckData deck)
    {
        bool b = false;
        for (int a = 0; a < deck.Heros.Length; a++)
        {
            if(deck.Heros[a].UnitUid == null)
            {
                b = true;
                return b; 
            }
        }
        for (int a = 0; a < deck.Dards.Length; a++)
        {
            if (deck.Dards[a] == null)
            {
                b = true;
                return b;
            }
        }
        return b; 
    }
}
