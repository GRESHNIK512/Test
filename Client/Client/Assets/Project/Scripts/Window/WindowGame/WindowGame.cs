using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowGame : Window
{
    [Inject] GameSettings _gameSettings;
    [Inject] MsgService _msgService;

    [SerializeField] Button[] _buttons;
    [SerializeField] WeatherContent _weatherContent;
    [SerializeField] FactsContent _factsContent;

    [SerializeField] Image _loadImg; 

    public void HandleMessage(NetworkMessage msg)
    {
        StopRotate();
        if (msg is WeatherMessage) _weatherContent.Refresh(msg);
        else _factsContent.Refresh(msg);
    }

    public void ShowMyContent(int value)
    {
        _weatherContent.Show(value == 0);
        _factsContent.Show(value == 1);
    }

    public void StopAllAnimationLoad()
    {
        _factsContent.StopAnim();
    }

    private void StartRotate()
    {
        _loadImg.enabled = true;
      
        _loadImg.transform.DORotate(_gameSettings.rotationAmount, _gameSettings.duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear) 
                 .SetLoops(-1, LoopType.Restart); 
    }

    public void StopRotate()
    {
        _loadImg.enabled = false;
        DOTween.Pause(_loadImg.transform);
    }

    public void UIButtonUpdateChoose(int targetButtonId)
    {  
        _msgService.CheckRepeatButton(targetButtonId);
        _factsContent.StopAnim();

        foreach (var button in _buttons)
        {
            button.UpdateButtonState(button.Id == targetButtonId);
        }

        StartRotate();
    } 
}