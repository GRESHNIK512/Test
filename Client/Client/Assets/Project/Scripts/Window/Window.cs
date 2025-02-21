using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private Canvas[] _allCanvas;
    [SerializeField] private GraphicRaycaster[] _graphicRayCasters;

    public void Show(bool show)
    {
        foreach (var canvas in _allCanvas)
        {
            if (canvas) canvas.enabled = show;
        }

        foreach (var rayCaster in _graphicRayCasters)
        {
            if (rayCaster) rayCaster.enabled = show;
        }  
    }
}