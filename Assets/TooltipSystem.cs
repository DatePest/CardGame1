using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipSystem : Singleton_T_Mono<TooltipSystem>
{
    [SerializeField] Tooltip_Text tooltip_Text;
    [SerializeField] Tooltip_CardDisplay tooltip_CardDisplay;
    [SerializeField] Tooltip_Unit_Display tooltip_Unit_Display;
    public bool Look { get; private set; } = false;
    public Tooltip_Text Tooltip_Text => tooltip_Text;
    public Tooltip_CardDisplay Tooltip_CardDisplay => tooltip_CardDisplay;
    public Tooltip_Unit_Display Tooltip_Unit_Display => tooltip_Unit_Display;
    private void Start()
    {
        //tooltip_Text = GetComponentInChildren<Tooltip_Text>();
        //tooltip_CardDisplay = GetComponentInChildren<Tooltip_CardDisplay>();
        //tooltip_Unit_Display = GetComponentInChildren<Tooltip_Unit_Display>();
        DontDestroyOnLoad(gameObject);
    }

    public void LookCn(bool b)
    {
        Look = b;
    }
    public void CloseAll()
    {
        tooltip_Unit_Display.Close();
        tooltip_CardDisplay.Close();
        Look = false;
    }
}

public class TooltipBase : MonoBehaviour
{
    
    private  void Start()
    {
        gameObject.SetActive(false);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    public bool IsDisplayIsActive()
    {
        return gameObject.activeSelf;
    }
}
