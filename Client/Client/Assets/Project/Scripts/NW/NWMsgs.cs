using Mirror;
using System.Collections.Generic;

//on Client
public struct ButtonClickMessage : NetworkMessage
{
    public int ID;
}

public struct ButtonClickFactMessage : NetworkMessage
{
    public string ID;
}

public struct WeatherMessage : NetworkMessage
{
    public string IconUrl;
    public string When;
    public string Temperature;
}

public struct BreedDataMessage : NetworkMessage
{
    public List<BreedData> BreedDataList;
}

public struct BreedDescriptionMessage : NetworkMessage
{
    public BreedData BreedData;
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