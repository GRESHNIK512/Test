using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GraphicRaycaster _graphicRayCaster; 
   
    public void Show(bool value)
    {
        if (_canvas) _canvas.enabled = value;
        if (_graphicRayCaster) _graphicRayCaster.enabled = value;
    }
}