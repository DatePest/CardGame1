using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BattleActionPool : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Image attackUnitIcon, damgerUnitIcon , BackGround;
    [SerializeField] TextMeshProUGUI text;
    AttatkBase AttatkAction;
    public Image AttackUnitIcon => attackUnitIcon;
    public Image DamgerUnitIcon => damgerUnitIcon;
    public TextMeshProUGUI GUIText => text;

    public void SetAttatkAction(AttatkBase attatk)
    {
        AttatkAction = attatk;
        attatk.UIShow(this);
    }

    public void Clear()
    {
        BG_White_Color();
        attackUnitIcon.sprite = null;
        damgerUnitIcon.sprite = null;
        GUIText.text = null;
        AttatkAction = null;
    }

    public void BG_Red_Color()
    {
        BackGround.color = new(255, 100, 100);
    }
    public void BG_White_Color()
    {
        BackGround.color = new(255, 255, 255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AttatkAction == null) return;
        AttatkAction.SkillData.CurrentUsePlayer.UserMouseManager.SetCurrentMaps(AttatkAction.GetMaps());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (AttatkAction == null) return;
        AttatkAction.SkillData.CurrentUsePlayer.UserMouseManager.ExitMapShow();
    }
}
