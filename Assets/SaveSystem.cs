using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using System;

public static class SaveSettion
{
    public static string extension = ".deck";
    public static string SavePath = Application.dataPath + "/Save/";
}

public class SaveSystem : MonoBehaviour
{
    [SerializeField] Deck_HeroEdit_Position HeroData;
    [SerializeField]  Deck_HeroEdit_Dropdown deck_HeroEdit_Dropdown;
    [SerializeField] CardDeckCustom_Deck Deck;
    [SerializeField] TextMeshProUGUI DeckName;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] LoadScreen loadScreen;
     DeckData CurrentDeckData;
    string extension => SaveSettion.extension;
    string SavePath => SaveSettion.SavePath;

    string CurrenSaveDataPath;

    private void Start()
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }
        AutoSaveName();
    }
    void CreateFile(string path)
    {
        if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);
        
        CurrenSaveDataPath = path;
        var data = new DeckData();
        CurrentDeckData = data;
        SaveData(CurrentDeckData);
        var stream = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new();
        formatter.Serialize(stream, CurrentDeckData);
        stream.Close();
    }
    void AutoSaveName()
    {
        DateTime currentDateTime = DateTime.Now;
        inputField.text = currentDateTime.ToString("yyyy-MM-dd-HH-mm-ss");
    }
    public  void SaveDeckData() //UnityGuiEventUse
    {
        if (inputField.text == null) { AutoSaveName(); return; }
        string path = SavePath + inputField.text + extension;
        CreateFile(path);
       
    }

    void  SaveData(DeckData data)
    {
        var CardsID = Deck.ReturnCards();
        for (int a = 0; a < CardsID.Length; a++)
        {
            data.Dards[a] = CardsID[a];
        }
        for (int a = 0; a < HeroData.HeroDatas.Length; a++)
        {
            //if (HeroData.HeroDatas[a].Uid == null) continue;
            data.Heros[a].UnitUid = HeroData.HeroDatas[a].Uid;
            data.Heros[a].MapPoint = HeroData.HeroDatas[a].Position;
        }
    }
    public void ShowLoadScreen()
    {
        loadScreen.gameObject.SetActive(true);
        
        var Names = Directory.GetFiles(SavePath);
        loadScreen.Show(Names, LoadDeckData, extension);
    }

    public void LoadDeckData(string path)
    {
        loadScreen.gameObject.SetActive(false);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            var stream = new FileStream(path, FileMode.Open);
            var data = formatter.Deserialize(stream) as DeckData;
            
            CurrentDeckData = data;
            stream.Close();

            CurrenSaveDataPath = path;
            deck_HeroEdit_Dropdown.DataLoad(CurrentDeckData);
            HeroData.LoadDataPostint(CurrentDeckData);
            Deck.LoadDataToDeck(CurrentDeckData);
            //DeckName.text = Path.GetFileNameWithoutExtension(path);
            inputField.text = Path.GetFileNameWithoutExtension(path);
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
        }
    }

    public void ExitScene()
    {
        GameManager.Instance.SceneManager.EnableNetSceneManager();
        GameManager.Instance.SceneManager.LocalLoadSceneAsync(Scene.TestMain);
    }
}
