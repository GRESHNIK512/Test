using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SwitchButton : Button
{
    [Inject] GameSettings _gameSettings;   
    
    public override void Start()
    {
        base.Start();
        UpdateButtonColor(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);  
    }

    public override void UpdateButtonColor(bool active)
    {
        _image.color = active ? _gameSettings.activeColor : _gameSettings.inactiveColor;
    } 
} 