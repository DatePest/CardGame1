using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TS_script : MonoBehaviour
{
    [SerializeField] Sprite[] Sprites;
    [SerializeField] RawImage Images;

    private void Awake()
    {
        Images = GetComponent<RawImage>();
    }
    private void Start()
    {
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action += UpdataRawImage;
        UpdataRawImage(CardGameManager.Instance.Ts_Manager.TsTimes);
    }
    
    private void Ond()
    {
        if(CardGameManager.Instance ==null) return;
        CardGameManager.Instance.Ts_Manager.TS_UpData_Action -= UpdataRawImage;
    }

    public void UpdataRawImage(int T)
    {
        Images.texture = Sprites[T].texture;
    }
}


