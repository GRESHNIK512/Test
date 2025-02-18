using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public abstract class Button : MonoBehaviour, IPointerClickHandler
{  
    public int Id { get; protected set; }
    [SerializeField] protected Image _image;   

    public virtual void OnPointerClick(PointerEventData eventData)
    { }

    public virtual void UpdateButtonState(bool active) 
    { }
} 