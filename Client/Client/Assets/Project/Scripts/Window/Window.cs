using Mirror;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    private Canvas _canvas;
    private GraphicRaycaster _rayCaster;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _rayCaster = GetComponent<GraphicRaycaster>();
    }
    public void Show(bool value)
    {
        _canvas.enabled = value;
        _rayCaster.enabled = value;
    }
}