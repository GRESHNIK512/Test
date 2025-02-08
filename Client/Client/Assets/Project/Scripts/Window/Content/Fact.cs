using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class Fact : MonoBehaviour, IPointerClickHandler
{
    [Inject] MsgService _msgService;
    [Inject] GameSettings _gameSettings;

    [SerializeField] private TextMeshProUGUI _infoTMP;
    [SerializeField] private Image _loadImage;  
    public string Id { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        _msgService.SendToClientFactButtonClickMsg(Id);
        StartRotate();
    }

    public void SetInfo(string value) 
    {
        _infoTMP.text = value;
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