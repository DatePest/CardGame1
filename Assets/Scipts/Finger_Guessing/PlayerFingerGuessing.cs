using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Netcode;
using System;

public class PlayerFingerGuessing : MonoBehaviour
{
    [SerializeField] RawImage scissors, rock, paper , ResultOwn, ResultEnemy , First, Back; // 1=ôíìÅ 2 =ïz 3 = êŒì™ 
    PlayerOBJ Myplayer;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void AddButtonTrigger(GameObject g, Action<PointerEventData> action)
    {
        EventTrigger eventTrigger = g.AddComponent<EventTrigger>();
        EventTrigger.Entry onButton = new EventTrigger.Entry();
        onButton.eventID = EventTriggerType.PointerDown;
        onButton.callback.AddListener((data) => { action((PointerEventData)data); });
        eventTrigger.triggers.Add(onButton);
    }
    public void SetSeletOBJ(PlayerOBJ player)
    {
        Myplayer = player;
        AddButtonTrigger(scissors.gameObject, SetSeletScissors);
        AddButtonTrigger(rock.gameObject, SetSeletRock);
        AddButtonTrigger(paper.gameObject, SetSeletPaper);
        AddButtonTrigger(First.gameObject, SetSeletFirst);
        AddButtonTrigger(Back.gameObject, SetSeletBack);
        scissors.transform.parent.gameObject.SetActive(false);
        ResultEnemy.transform.parent.gameObject.SetActive(false);
        First.transform.parent.gameObject.SetActive(false);
    }
    void SetSeletScissors(PointerEventData data)
    {
        CardGameManager.Instance.FInger_Guessing.ReturnPlayerFingerServerRpc(NetworkManager.Singleton.LocalClientId, 1);
        scissors.transform.parent.gameObject.SetActive(false);
    }
    void SetSeletRock(PointerEventData data)
    {
        CardGameManager.Instance.FInger_Guessing.ReturnPlayerFingerServerRpc(NetworkManager.Singleton.LocalClientId, 2);
        scissors.transform.parent.gameObject.SetActive(false);
    }
    void SetSeletPaper(PointerEventData data)
    {
        CardGameManager.Instance.FInger_Guessing.ReturnPlayerFingerServerRpc(NetworkManager.Singleton.LocalClientId, 3);
        scissors.transform.parent.gameObject.SetActive(false);
    }
    void SetSeletFirst(PointerEventData data)
    {
        First.transform.parent.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        CardGameManager.Instance.FInger_Guessing.SetPlayersOrderServerRpc(true);
    }
    void SetSeletBack(PointerEventData data)
    {
        First.transform.parent.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        CardGameManager.Instance.FInger_Guessing.SetPlayersOrderServerRpc(false);
    }

    public void FingerGuessingStart()
    {
        scissors.transform.parent.gameObject.SetActive(true);
    }

    public void RunSelectOrder()
    {
        First.transform.parent.gameObject.SetActive(true);
    }

    RawImage Get_GRawImage(int I)
    {
        if (I == 1)
            return scissors;
        else if (I == 2)
            return rock;
        else 
            return paper;
    }
    public void FingerGuessingResult(int Own , int Enemy)
    {
        ResultOwn.texture = Get_GRawImage(Own).texture;
        ResultEnemy.texture = Get_GRawImage(Enemy).texture;
        StartCoroutine(Show());
    }
    IEnumerator Show()
    {
        ResultEnemy.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        ResultEnemy.transform.parent.gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        Myplayer = null;
    }

}
