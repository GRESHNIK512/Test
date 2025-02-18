using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class BreedService
{
    private const string ApiUrl = "https://dogapi.dog/api/v2/breeds";
    public BreedData[] Breeds { get; private set; } 
    public bool IsLoadedData { get; private set; }

    public async UniTask FetchBreedsAsync()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ApiUrl))
        {
            await webRequest.SendWebRequest();  

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || 
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {webRequest.error}"); 
            } 
           
            var responseJson = webRequest.downloadHandler.text;
            var apiResponse = JsonUtility.FromJson<ApiResponse>(responseJson);
            
            Breeds = apiResponse.data; 
            IsLoadedData = true;  
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
    public BreedData[] data; 
}
