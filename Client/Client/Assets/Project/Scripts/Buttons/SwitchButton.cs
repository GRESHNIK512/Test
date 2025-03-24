using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class SwitchButton : Button
{  
    public event Action<int> OnClickButtonWithIdEvent;
    public int Id => (int)_type; 
   
    [SerializeField] private Image _image;
    [SerializeField] private SwitchTypeButton _type;

    private GameSettings _gameSettings;
    private bool _isChooseen;   

    [Inject]
    public void Construct(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
        UpdateButtonState(false);
    }   

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (!_isChooseen)
        {
            OnClickButtonWithIdEvent?.Invoke(Id); 
        }

        _isChooseen = true;
    }

    public void UpdateButtonState(bool active)
    {
        _image.color = active ? _gameSettings.ActiveColor : _gameSettings.InactiveColor;

        if (!active)
        {
            _isChooseen = false;
        }
    }
} 