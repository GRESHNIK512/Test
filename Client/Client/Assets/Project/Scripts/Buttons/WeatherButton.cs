using UnityEngine.EventSystems;
using Zenject;

public class WeatherButton : SwitchButton
{
    [Inject] WindowGame _windowgame;

    void Start()
    {
        Id = 0;  // ������
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
       
        if (!_isChooseen)
        {
            _windowgame.UIButtonUpdateChoose(Id);
        }

        _isChooseen = true;
    }
}