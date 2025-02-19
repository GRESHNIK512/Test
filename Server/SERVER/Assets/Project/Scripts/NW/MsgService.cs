using Mirror;
using System.Linq;
using UnityEngine;


public class MsgService : MonoBehaviour
{
    private BreedService _breedService = new ();
    private WeatherService _weatherService = new (); 
   
    #region RegisterMsg
    public void RegistersMsg()
    {
        NetworkServer.RegisterHandler<ButtonClickMessage>(OnReceiveButtonClickMessage);
        NetworkServer.RegisterHandler<ButtonClickFactMessage>(OnReceiveButtonFactClickMessage); 
    }
    #endregion

    private void OnReceiveButtonClickMessage(NetworkConnectionToClient conn, ButtonClickMessage message)
    {
        Debug.Log("Msg RecivedFromClient ButtonClick " + message.Id);
        switch (message.Id)
        {
            case 0: SendWeatherToClientByConn(conn); break; //pogoda
            case 1: SendBreedsToClientByConn(conn);  break; //fact
            default: Debug.LogError("No valid ID ButtonClickMsg"); break;
        }
    }

    private void OnReceiveButtonFactClickMessage(NetworkConnectionToClient conn, ButtonClickFactMessage message)
    {
        Debug.Log("Msg RecivedFromClient ButtonClickFact" + message.Id);  
        var msg = new BreedDescriptionMessage()
        {
            BreedData = _breedService.GetBreedByID(message.Id),
            UnqIdMsg = message.UnqIdMsg 
        };

        NetworkServer.SendToConn(msg, conn);
    } 

    #region SendToCLient

    private async void SendWeatherToClientByConn(NetworkConnectionToClient conn)
    {
        Debug.Log("Msg SendWeatherToClientByConn");
        if (!_weatherService.IsLoadedData) await _weatherService.FetchWeatherData();
        var period = _weatherService.GetData(); 
        var msg = new WeatherMessage()
        { 
              ByteSprite = period.sprite.texture.EncodeToJPG(),
              When = period.name,
              Temperature = $"{period.temperature}{period.temperatureUnit}"
        };  
        NetworkServer.SendToConn(msg, conn);
    }

    private async void SendBreedsToClientByConn(NetworkConnectionToClient conn)
    {
        if (!_breedService.IsLoadedData) await _breedService.FetchBreedsAsync();
        var msg = new BreedDataMessage()
        { 
            BreedsData = _breedService.Breeds
        };
        //Debug.Log($"{msg.BreedsData.Length}");
        NetworkServer.SendToConn(msg, conn);
    } 
    #endregion 
}