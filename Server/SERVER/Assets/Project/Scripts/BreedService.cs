using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class BreedService : MonoBehaviour
{  
    private const string ApiUrl = "https://dogapi.dog/api/v2/breeds";
    public List<BreedData> Breeds { get; private set; } = new();  

    async void Start()
    {
        Breeds = await FetchBreedsAsync(); 
    }

    async Task<List<BreedData>> FetchBreedsAsync()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ApiUrl))
        {
            await webRequest.SendWebRequest();  

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}");
                return null;
            }

            var responseJson = webRequest.downloadHandler.text;
            var response = JsonUtility.FromJson<ApiResponse>(responseJson);

            return response.data;
        }
    } 

    public BreedData GetBreedByID(string id) 
    {
        foreach (var breed in Breeds) 
        {
            if (breed.id == id) return breed;
        }
        return null;
    } 
}

[System.Serializable]
public class BreedData
{
    public string id; 
    public BreedAttributes attributes;
}

[System.Serializable]
public class BreedAttributes
{
    public string name;
    public string description; 
}

[System.Serializable]
public class ApiResponse
{
    public List<BreedData> data; 
}
