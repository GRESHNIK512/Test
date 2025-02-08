using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherService : MonoBehaviour
{
    int partNumber = 0; 

    private WeatherData _weatherData;

    private string apiUrl = "https://api.weather.gov/gridpoints/TOP/31,80/forecast";

    private async void Start()
    {
        await FetchWeatherData();
    }

    private async UniTask FetchWeatherData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                _weatherData = JsonUtility.FromJson<WeatherData>(jsonResponse);
            }
        }
    }  

    public Period GetData() 
    {  
        if (partNumber >= _weatherData.properties.periods.Count()) partNumber = 0; 
        return _weatherData.properties.periods[partNumber++];
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