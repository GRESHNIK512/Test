using UnityEngine.EventSystems;
using Zenject;

public class WeatherButton : SwitchButton
{
    [Inject] WindowGame _windowgame;
    [Inject] MsgService _msgService;

    public override void Start()
    {
        base.Start();
        Id = 0;  // Погода
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        _windowgame.UIButtonUpdateChoose(Id);
        _msgService.SendToClientButtonClickMsg(Id);
    }
} 