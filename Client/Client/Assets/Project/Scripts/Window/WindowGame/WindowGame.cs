using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowGame : Window
{
    private GameSettings _gameSettings;
    private MsgService _msgService;

    [SerializeField] private SwitchButton[] _switchButtons;
    [SerializeField] private WeatherContent _weatherContent;
    [SerializeField] private FactsContent _factsContent;

    [SerializeField] private Image _loadImg;
    private Tween _rotationTween;

    [Inject]
    public void Construct(GameSettings gameSettings, MsgService msgService)
    {
        _gameSettings = gameSettings;
        _msgService = msgService;
       
        foreach (var button in _switchButtons) 
        {
            button.OnClickButtonWithIdEvent += UIButtonUpdateChoose;
        }
    }

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

        if (_rotationTween == null)
        {
            _rotationTween = _loadImg.transform.DORotate(
                _gameSettings.RotationAmount,
                _gameSettings.Duration,
                RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).Pause();
        }

        _rotationTween.Play();
    }

    public void StopRotate()
    {
        _loadImg.enabled = false;
       
        if (_rotationTween != null && _rotationTween.IsPlaying())
        {
            _rotationTween.Pause();
        }
    }

    public void UIButtonUpdateChoose(int targetButtonId)
    {  
        _msgService.UserClickOnButtonWithId(targetButtonId);
        _factsContent.StopAnim();

        foreach (var button in _switchButtons)
        {
            button.UpdateButtonState(button.Id == targetButtonId);
        }

        StartRotate();
    } 
}