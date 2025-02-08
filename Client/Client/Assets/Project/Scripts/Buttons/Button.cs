using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public abstract class Button : MonoBehaviour, IPointerClickHandler
{  
    public int Id { get; protected set; }
    protected Image _image;


    public virtual void Start() 
    {
        _image = GetComponent<Image>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    { 
         
    }

    public virtual void UpdateButtonColor(bool active) 
    {
    
    }
} 