using Mirror;
using UnityEngine;
using Zenject;

public class MsgService : MonoBehaviour
{
    [Inject] WindowDescription _windowDescription;
    [Inject] WindowGame _windowgame; 

    #region RegisterMsg
    public void RegistersMsg()
    {
        NetworkClient.RegisterHandler<WeatherMessage>(OnReceiveWeatherMessage);
        NetworkClient.RegisterHandler<BreedDataMessage>(OnReceiveBreedDataMessage);
        NetworkClient.RegisterHandler<BreedDescriptionMessage>(OnReceiveBreedDescriptionMessage);
    }
    #endregion 

    //Отправка 
    public void SendToClientButtonClickMsg(int id) 
    {
        Debug.Log("Отправка на Сервер клик по Меню");
        NetworkClient.Send(new ButtonClickMessage() { ID = id });
    }

    public void SendToClientFactButtonClickMsg(string id)
    {
        Debug.Log("Отправка на Сервер клик по Fact");
        NetworkClient.Send(new ButtonClickFactMessage() { ID = id });
    }

    //Обработка
    public void OnReceiveWeatherMessage(WeatherMessage message)
    {
        _windowgame.HandleMessage(message); 
    }

    public void OnReceiveBreedDataMessage(BreedDataMessage message)
    {
        Debug.Log("Сообщение с сервера общий список фактов");
        _windowgame.HandleMessage(message);
        
    }
    public void OnReceiveBreedDescriptionMessage(BreedDescriptionMessage message)
    {
        Debug.Log("Сообщение с сервера факт + подробности");
        _windowDescription.RefreshInfo(message);
    } 
} 