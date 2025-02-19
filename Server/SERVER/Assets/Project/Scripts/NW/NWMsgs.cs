using Mirror;

public struct ButtonClickMessage : NetworkMessage
{
    public int Id;
}

public struct ButtonClickFactMessage : NetworkMessage 
{
    public int UnqIdMsg;
    public string Id; 
}

public struct WeatherMessage : NetworkMessage 
{
    public byte[] ByteSprite; 
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