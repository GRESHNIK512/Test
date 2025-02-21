using Mirror;
using UnityEngine;
using UnityEngine.UI;

public abstract class Content : MonoBehaviour
{
    [SerializeField] private Canvas[] _allCanvas;
    [SerializeField] private GraphicRaycaster[] _graphicRayCasters;

    private void Start()
    {
        Show(false);
    }

    public abstract void Refresh(NetworkMessage msg);

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
