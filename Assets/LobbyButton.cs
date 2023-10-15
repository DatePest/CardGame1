using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LobbyScene
{
    public class LobbyButton : MonoBehaviour
    {
        [SerializeField] Button Ready, Exit, _Start,LobbyIDCopy;
        Dictionary<byte, Button> ButtonDictionary = new();
        // Start is called before the first frame update
        void Awake()
        {
            //ButtonDictionary = new();
            Ready.onClick.AddListener(() => LobbyScene_Ctrl.Instance.ReadyButton());
            Exit.onClick.AddListener(() => LobbyScene_Ctrl.Instance.LobbyExitButton());
            _Start.onClick.AddListener(() => LobbyScene_Ctrl.Instance.GameStartButton());
            LobbyIDCopy.onClick.AddListener(() => LobbyScene_Ctrl.Instance.CopyLobbyIDButton());
            ButtonDictionary.Add((byte)ButtonDictionary.Count, Ready);
            ButtonDictionary.Add((byte)ButtonDictionary.Count, Exit);
            ButtonDictionary.Add((byte)ButtonDictionary.Count, _Start);
            ButtonDictionary.Add((byte)ButtonDictionary.Count, LobbyIDCopy);

        }

        public Button GetButton(LobbyButton_ button_)
        {
            if (ButtonDictionary.TryGetValue((byte)button_, out var b))
                return b;
            else
            {
                Debug.LogError("LobbyButton_ Is null "+ button_.ToString());
                return null;
            }
        }
    }

    public enum LobbyButton_
    {
        Ready =0,
        Exit =1,
        _Start=2, 
        LobbyIDCopy =3
    }
}

