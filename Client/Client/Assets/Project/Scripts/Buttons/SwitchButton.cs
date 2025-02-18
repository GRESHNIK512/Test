using UnityEngine.EventSystems;
using Zenject;

public class SwitchButton : Button
{
    [Inject] GameSettings _gameSettings;
    protected bool _isChooseen;
   
    void Start()
    { 
        UpdateButtonState(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    { 
        base.OnPointerClick(eventData); 
    }

    public override void UpdateButtonState(bool active)
    { 
        _image.color = active ? _gameSettings.activeColor : _gameSettings.inactiveColor;
        if (!active) _isChooseen = false;
    } 
} 