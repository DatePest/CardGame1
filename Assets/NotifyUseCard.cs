using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyUseCard : MonoBehaviour
{
    Coroutine Notify;
    RawImage rawImage;
    private void Awake()
    {
        rawImage = GetComponentInChildren<RawImage>();
        gameObject.SetActive(false);
        CardGameManager.Instance.GameNotifyAction_Net.CardUse += notifyUseCrad;
        CardGameManager.Instance.GameNotifyAction_Net.Unit += notifyUnitSkill;
    }
    private void OnDisable()
    {
        CardGameManager.Instance.GameNotifyAction_Net.CardUse -= notifyUseCrad;
        CardGameManager.Instance.GameNotifyAction_Net.Unit -= notifyUnitSkill;
    }
    private void notifyUnitSkill(Unit U, float Time)
    {
        gameObject.SetActive(true);
        if (U.UnitData.cardArt.texture != null)
            rawImage.texture = U.UnitData.cardArt.texture;
        if (Notify != null)
        {
            StopCoroutine(Notify);
        }
        Notify = StartCoroutine(notifyUseCradWait(Time));
    }

    public void notifyUseCrad(CardSolt currentCradslot, float Time)
    {
        gameObject.SetActive(true);
        if (currentCradslot.CardSO.cardArt != null)
            rawImage.texture = currentCradslot.CardSO.cardArt.texture;
        if (Notify != null)
        {
            StopCoroutine(Notify);
        }
        Notify = StartCoroutine(notifyUseCradWait(Time));

    }

    IEnumerator notifyUseCradWait(float T)
    {
        yield return new WaitForSeconds(T);
        gameObject.SetActive(false);
        Notify = null;
    }
}
