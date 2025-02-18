using Mirror;

public struct ButtonClickMessage : NetworkMessage, INetworkSender
{
    public int Id; 
    public void Send() { NetworkClient.Send(this); }
}

public struct ButtonClickFactMessage : NetworkMessage, INetworkSender
{
    public int UnqIdMsg;
    public string Id; 
    public void Send() { NetworkClient.Send(this); } 
}

public struct WeatherMessage : NetworkMessage 
{
    public string IconUrl;
    public string When;
    public string Temperature;  
}

public struct BreedDataMessage : NetworkMessage 
{ 
    public BreedData[] BreedsData; 
}

public struct BreedDescriptionMessage : NetworkMessage
{
    public int UnqIdMsg;
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

public interface INetworkSender
{
    void Send(); 
}

 