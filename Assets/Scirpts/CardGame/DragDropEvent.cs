using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragDropEvent
{
    public void SetRaycastTarget(MonoBehaviour gameObject, bool b)
    {
        Image[] allImages;

        allImages = gameObject.GetComponentsInChildren<Image>();
        gameObject.GetComponent<Image>().raycastTarget = b;
        foreach (var result in allImages)
        {
            result.raycastTarget = b;
        }

    }
    public void SetCardAlpha(MonoBehaviour gameObject, float F)
    {
        Image[] allImages;
        Color color = gameObject.GetComponent<Image>().color;
        color.a = F;
        gameObject.GetComponent<Image>().color = color;
        allImages = gameObject.GetComponentsInChildren<Image>();
        foreach (var result in allImages)
        {
            color = result.color;
            color.a = F;
            result.color = color;
        }
        TextMeshProUGUI[] allTextes;
        allTextes = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var result in allTextes)
        {
            color = result.color;
            color.a = F;
            result.color = color;
        }

    }
}
