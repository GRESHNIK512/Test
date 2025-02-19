using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image _image;
    public int Id { get; protected set; }

    public virtual void OnPointerClick(PointerEventData eventData)
    { }

    public virtual void UpdateButtonState(bool active)
    { }
}