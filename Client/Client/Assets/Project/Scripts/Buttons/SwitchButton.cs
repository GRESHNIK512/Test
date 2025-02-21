using UnityEngine.EventSystems;
using Zenject;

public class SwitchButton : Button
{
    private GameSettings _gameSettings;
    protected bool _isChooseen;  

    [Inject]
    public void Construct(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

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
        _image.color = active ? _gameSettings.ActiveColor : _gameSettings.InactiveColor;

        if (!active)
        {
            _isChooseen = false;
        }
    }
}