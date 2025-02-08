using UnityEngine.EventSystems;
using Zenject;

public class FactsButton : SwitchButton
{
    [Inject] WindowGame _windowgame;
    [Inject] MsgService _msgService;

    public override void Start()
    {
        base.Start();
        Id = 1;  //Факты
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        _windowgame.UIButtonUpdateChoose(Id);
        _msgService.SendToClientButtonClickMsg(Id);
    }
} 