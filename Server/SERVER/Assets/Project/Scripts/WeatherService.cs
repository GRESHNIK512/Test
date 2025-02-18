using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherService 
{
    private int _periodNumber = 0; 
    private WeatherData _weatherData;
    public bool IsLoadedData { get; private set; }

    private string ApiUrl = "https://api.weather.gov/gridpoints/TOP/31,80/forecast";  

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
                
                IsLoadedData = true; 
            }
        }
    }  

    public Period GetData() 
    {  
        if (_periodNumber >= _weatherData.properties.periods.Count()) _periodNumber = 0; 
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
} 