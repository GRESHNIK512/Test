using UnityEngine.EventSystems;
using Zenject;

public class OKButton : Button
{
    [Inject] Screen _windowService;
    [Inject] WindowGame _windowgame;
    public override void OnPointerClick(PointerEventData eventData)
    {
        _windowService.ShowOnlyMe(_windowgame);
        _windowgame.StopAllAnimationLoad();
    } 
} 