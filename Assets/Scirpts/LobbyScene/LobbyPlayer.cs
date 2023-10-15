using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using System;
namespace LobbyScene
{
    public class LobbyPlayer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] RawImage isReady;
        //[SerializeField] bool IsReady=false;
        public bool IsReady { get; private set; }
        public ulong OwnerID { get; private set; }

        public void SetOwner(ulong Id, string Name)
        {
            //Debug.Log("SetOwner");
            gameObject.SetActive(true);
            OwnerID = Id;
            playerName.text = Name;
            IsReady = false;
        }
        public void LeaveOwnerSc()
        {
            // OwnerID = null;
            gameObject.SetActive(false);
            IsReady = false;
        }
        public void IsReadyUpdata() // Loacl
        {
            IsReady = !IsReady;
            LobbyScene_Ctrl.Instance.HideLoadDeckButton(IsReady);
            LobbyScene_Ctrl.Instance.PlayerReady(IsReady);

        }
        public void SetIsReady(bool b)
        {
            IsReady = b;
            ReadyColor();
            if (NetworkManager.Singleton.IsHost)
            {
                LobbyScene_Ctrl.Instance.CheckAllReady();
            }
        }
        void ReadyColor()
        {
            if (IsReady == true)
                isReady.color = Color.green;
            else
                isReady.color = Color.red;
        }
    }
}
