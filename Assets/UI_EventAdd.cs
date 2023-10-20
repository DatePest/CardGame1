using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EventAdd : MonoBehaviour
{
    [SerializeField]  E_Button[] buttons;

    public void RStart()
    {
        foreach(var a in buttons)
        {
            a.Start(this.gameObject);
        }
        gameObject.SetActive(false);
    }
    public Button GetButton(int i) => buttons[i].GetButton();

}
[System.Serializable]
public class E_Button
{
    [SerializeField]  Button button;
    [SerializeField] byte ActionID;

    public void Start(GameObject g)
    {
        button.onClick.AddListener(() => CardGame_PlayerUIManager.Instance.UseButtonAction(ActionID,g));
    }
    public Button GetButton() => button;

}
