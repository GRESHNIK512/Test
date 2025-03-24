using UnityEngine;
using UnityEngine.EventSystems; 

public abstract class Button : MonoBehaviour, IPointerClickHandler
{   
    public virtual void OnPointerClick(PointerEventData eventData)
    { }  
}

public enum SwitchTypeButton
{
    Weather,
    Facts
}