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
    [Inject] FactsContent _factsContent;

    [SerializeField] private TextMeshProUGUI _infoTMP;
    [SerializeField] private Image _loadImg;
    public string Id { get; set; }

    private bool _choosen; 
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_choosen) return;

        _msgService.SendToClientFactButtonClickMsg(Id);
        StartRotate();
        _choosen = true;
        _factsContent.ResetAllFactIgnoreMe(this);
    }

    public void SetInfo(string value)
    {
        _infoTMP.text = value;
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
        _choosen = false;
        _loadImg.enabled = false;
        DOTween.Pause(_loadImg.transform);
    } 
}