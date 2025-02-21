using UnityEngine.EventSystems;
using Zenject;

public class OkButton : Button
{
    private Screen _screen; 
    private WindowGame _windowGame;

    [Inject]
    public void Construct(WindowGame windowGame, Screen screen)
    {
        _windowGame = windowGame;
        _screen = screen;
    }

    public override void OnPointerClick(PointerEventData eventData)
    { 
        _windowGame.StopAllAnimationLoad();
        _screen.ShowOnlyMe(_windowGame);
    }
}