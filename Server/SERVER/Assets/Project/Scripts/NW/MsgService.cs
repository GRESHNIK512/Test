using Mirror;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MsgService : MonoBehaviour
{
    [Inject] BreedService _breedService;
    [Inject] WeatherService _weatherService;

    #region RegisterMsg
    public void RegistersMsg()
    {
        NetworkServer.RegisterHandler<ButtonClickMessage>(OnReceiveButtonClickMessage);
        NetworkServer.RegisterHandler<ButtonClickFactMessage>(OnReceiveButtonFactClickMessage); 
    }
    #endregion

    private void OnReceiveButtonClickMessage(NetworkConnectionToClient conn, ButtonClickMessage message)
    {
        Debug.Log("Msg RecivedFromClient ButtonClick " + message.ID);
        switch (message.ID)
        {
            case 0: SendWeatherToClientByConn(conn); break; //pogode
            case 1: SendBreedsToClientByConn(conn);  break; //fact
            default: Debug.LogError("No valid ID ButtonClickMsg"); break;
        }
    }

    private async void OnReceiveButtonFactClickMessage(NetworkConnectionToClient conn, ButtonClickFactMessage message)
    {
        Debug.Log("Msg RecivedFromClient ButtonClickFact " + message.ID);
        await Task.Delay(Random.Range(250, 500));
        var data = _breedService.GetBreedByID(message.ID);
        var msg = new BreedDescriptionMessage()
        {
            BreedData = data
        };

        NetworkServer.SendToConn(msg, conn);
    } 

    #region SendToCLient

    private async void SendWeatherToClientByConn(NetworkConnectionToClient conn)
    {
        await Task.Delay(Random.Range(250, 500));
        var period = _weatherService.GetData(); 
        var msg = new WeatherMessage()
        {
              IconUrl = period.icon,
              When = period.name,
              Temperature = $"{period.temperature}{period.temperatureUnit}"
        };  
        NetworkServer.SendToConn(msg, conn);
    }

    private async void SendBreedsToClientByConn(NetworkConnectionToClient conn)
    {
        await Task.Delay(Random.Range(250, 500));
        var msg = new BreedDataMessage()
        {
            BreedDataList = _breedService.Breeds
        };
       
        NetworkServer.SendToConn(msg, conn);
    }

    #endregion

}