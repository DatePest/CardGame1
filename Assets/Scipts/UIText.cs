using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MyText;
    public void SetTMPro(string s)
    {
        if(MyText == null)
        {
            MyText = gameObject.GetComponent<TextMeshProUGUI>();
        }

        if (MyText.gameObject.activeSelf == false) MyText.gameObject.SetActive(true);
        MyText.text = s;
    }
}
