using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WindowGame : Window
{
    [Inject] GameSettings _gameSettings;

    [SerializeField] Button[] _button;
    [SerializeField] WeatherContent _weatherContent;
    [SerializeField] FactsContent _factsContent;

    [SerializeField] Image _loadImage;  

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

    public void UIButtonUpdateChoose(int targetButtonId)
    {
        foreach (var button in _button)
        {
            button.UpdateButtonColor(button.Id == targetButtonId);
        }
        StartRotate();
    }

    public void StopAllAnimationLoad() 
    {
        _factsContent.StopAnim();
    }

    private void StartRotate()
    {
        _loadImage.enabled = true;
        // Используем DOTween для вращения объекта
        _loadImage.transform.DORotate(_gameSettings.rotationAmount, _gameSettings.duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear) // Линейное движение (постоянная скорость)
                 .SetLoops(-1, LoopType.Restart); // Бесконечное повторение
    }

    public void StopRotate()
    {
        _loadImage.enabled = false;
        DOTween.Pause(_loadImage.transform);
    }
}