using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Action<PointerEventData> OnClick;
    public Action<PointerEventData> OnEnter;
    public Action<PointerEventData> OnExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnClick != null)
            OnClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnEnter != null)
            OnEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnExit != null)
            OnExit(eventData);
    }
}
