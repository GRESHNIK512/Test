using Mirror;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Zenject;

public class WeatherContent : Content
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _whenTMP;
    [SerializeField] private TextMeshProUGUI _temperatureTMP;
    
    [Inject] WindowGame _windowgame;

    public async override void Refresh(NetworkMessage msg)
    {
        var Msg = (WeatherMessage)msg;

        try
        {
            // Загружаем иконку можно кешировать в словарь чтобы каждый раз не грузить
            Sprite iconSprite = await LoadIcon(Msg.IconUrl);

            if (_whenTMP != null) _whenTMP.text = Msg.When;
            else Debug.LogError("_whenTMP is not assigned.");

            if (_temperatureTMP != null) _temperatureTMP.text = Msg.Temperature;
            else Debug.LogError("_temperatureTMP is not assigned.");

            if (_image != null && iconSprite != null) _image.sprite = iconSprite;
            else Debug.LogError("_iconImage is not assigned or iconSprite is null.");

            _image.color = Color.white;
            _windowgame.ShowMyContent(0);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error refreshing weather data: {ex.Message}");
        }
    }

    private async Task<Sprite> LoadIcon(string iconUrl)
    {
        if (string.IsNullOrEmpty(iconUrl))
        {
            Debug.LogError("Icon URL is null or empty.");
            return null;
        }

        try
        {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(iconUrl))
            {

                var operation = webRequest.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Error loading icon from {iconUrl}: {webRequest.error}");
                    return null;
                }

                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

                Sprite iconSprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f)
                );

                return iconSprite;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception while loading icon: {ex.Message}");
            return null;
        }
    }
} 