using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class Fact : MonoBehaviour, IPointerClickHandler
{ 
    private GameSettings _gameSettings;
    private FactsContent _factsContent;

    [SerializeField] private TextMeshProUGUI _infoTMP;
    [SerializeField] private Image _loadImg;
    private Tween _rotationTween;  
    private bool _choosen;

    public string Id { get; set; }

    [Inject]
    public void Construct(FactsContent factsContent, GameSettings gameSettings)
    {
        _factsContent = factsContent;
        _gameSettings = gameSettings;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_choosen) return;
       
        _choosen = true;
        _factsContent.OnFactChosen(Id);
        _factsContent.ResetAllFactIgnoreMe(this);
        StartRotate(); 
    }

    public void SetInfo(string value)
    {
        _infoTMP.text = value;
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
        _choosen = false;
        _loadImg.enabled = false;
       
        if (_rotationTween != null && _rotationTween.IsPlaying())
        {
            _rotationTween.Pause();
        }
    } 
}