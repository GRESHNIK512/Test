using UnityEngine.EventSystems;
using Zenject;

public class FactsButton : SwitchButton
{
    [Inject] WindowGame _windowgame; 

    void Start()
    { 
        Id = 1;  //Факты
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (!_isChooseen) _windowgame.UIButtonUpdateChoose(Id);
        _isChooseen = true;
    }
} 