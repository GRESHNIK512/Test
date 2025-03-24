using System;
using UnityEngine.EventSystems;

public class OkButton : Button
{ 
    public event Action OnClickButton;  

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        OnClickButton?.Invoke(); 
    }
}