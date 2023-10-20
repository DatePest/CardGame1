using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground_IPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        sprite.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        sprite.color = Color.white;
    }
}
