using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeatherContent : Content
{
    private WindowGame _windowGame;

    [SerializeField] private Image _weatherImg;
    [SerializeField] private TextMeshProUGUI _whenTMP;
    [SerializeField] private TextMeshProUGUI _temperatureTMP;  

    [Inject]
    public void Construct(WindowGame windowGame, Screen screen)
    {
        _windowGame = windowGame; 
    }

    public override void Refresh(NetworkMessage msg)
    {
        var Msg = (WeatherMessage)msg;  

        if (_whenTMP != null) _whenTMP.text = Msg.When;
        else Debug.LogError("_whenTMP is not assigned.");

        if (_temperatureTMP != null) _temperatureTMP.text = Msg.Temperature;
        else Debug.LogError("_temperatureTMP is not assigned."); 
        
        if (_weatherImg != null ) _weatherImg.sprite = BytesToSprite(Msg.ByteSprite);
        else Debug.LogError("_iconImage is not assigned or iconSprite is null.");

        _weatherImg.color = Color.white;
        _windowGame.ShowMyContent(0); 
    }

    private Sprite BytesToSprite(byte[] bytes)
    { 
        Texture2D texture = new Texture2D(2, 2);  
        texture.LoadImage(bytes);   
        
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f) 
        );

        return sprite;
    } 
}