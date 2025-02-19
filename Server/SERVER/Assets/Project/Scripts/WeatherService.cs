using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherService
{
    private string ApiUrl = "https://api.weather.gov/gridpoints/TOP/31,80/forecast";
    private int _periodNumber = 0;
    private WeatherData _weatherData;
    public bool IsLoadedData { get; private set; }

    public async UniTask FetchWeatherData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ApiUrl))
        {
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }

            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                _weatherData = JsonUtility.FromJson<WeatherData>(jsonResponse);
                 
                foreach (var period in _weatherData.properties.periods)
                {
                    period.sprite = await LoadTexture(period.icon);
                }

                IsLoadedData = true;
            }
        }
    }

    private async UniTask<Sprite> LoadTexture(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading texture: " + webRequest.error);
                return null;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            return sprite;
        }
    }

    public Period GetData()
    {
        if (_weatherData?.properties?.periods == null || _weatherData.properties.periods.Length == 0)
        {
            Debug.LogError("Weather data is not loaded or empty.");
            return null;
        }

        if (_periodNumber >= _weatherData.properties.periods.Length)
        {
            _periodNumber = 0;
        }
               
        return _weatherData.properties.periods[_periodNumber++];
    }
}

[System.Serializable]
public class WeatherData
{
    public Properties properties;
}

[System.Serializable]
public class Properties
{
    public Period[] periods;
}

[System.Serializable]
public class Period
{
    public string name;
    public int temperature;
    public string temperatureUnit;
    public string icon;
    public Sprite sprite; // Добавляем поле для хранения текстуры
}