using UnityEngine.EventSystems;
using Zenject;

public class FactsButton : SwitchButton
{
    private WindowGame _windowGame;

    [Inject]
    public void Construct(WindowGame windowGame)
    {
        _windowGame = windowGame;
    }

    void Start()
    {
        Id = 1;  // Факты
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!_isChooseen) 
        {
            _windowGame.UIButtonUpdateChoose(Id);
        }

        _isChooseen = true;
    }
}