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
        tooltip_Text = Instantiate(Tooltip_Text, transform);
        tooltip_CardDisplay = Instantiate(tooltip_CardDisplay, transform);
        tooltip_Unit_Display = Instantiate(tooltip_Unit_Display, transform);
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

public abstract class TooltipBase : MonoBehaviour
{
    public abstract void Ins();
    private  void Start()
    {
        Ins();
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
