using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip_Text : TooltipBase
{
    [SerializeField] RectTransform CanvasRect;
    [SerializeField] TextMeshProUGUI TooltipText;
    [SerializeField] RectTransform Background;
    Vector2 onignal = new(400, 200);

    public void SetTooltipText(string Text)
    {
        gameObject.SetActive(true);
        Background.sizeDelta = onignal;
        TooltipText.SetText(Text);
        TooltipText.ForceMeshUpdate();
        Vector2 TextSize = TooltipText.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(25, 25);
        Background.sizeDelta = TextSize + paddingSize;
        SetPosition();
    }

    private void Update()
    {
        if (Background.gameObject.activeSelf != true) return;
        SetPosition();
    }

    void SetPosition()
    {
        Vector2 Position = Input.mousePosition / CanvasRect.localScale.x;
        if (Position.x + Background.rect.width > CanvasRect.rect.width)
            Position.x = CanvasRect.rect.width - Background.rect.width;
        if (Position.y + Background.rect.height > CanvasRect.rect.height)
            Position.y = CanvasRect.rect.height - Background.rect.height;
        Background.anchoredPosition = Position;
    }
}
