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

    //�������� 
    public void SendToClientButtonClickMsg(int id) 
    {
        Debug.Log("�������� �� ������ ���� �� ����");
        NetworkClient.Send(new ButtonClickMessage() { ID = id });
    }

    public void SendToClientFactButtonClickMsg(string id)
    {
        Debug.Log("�������� �� ������ ���� �� Fact");
        NetworkClient.Send(new ButtonClickFactMessage() { ID = id });
    }

    //���������
    public void OnReceiveWeatherMessage(WeatherMessage message)
    {
        _windowgame.HandleMessage(message); 
    }

    public void OnReceiveBreedDataMessage(BreedDataMessage message)
    {
        Debug.Log("��������� � ������� ����� ������ ������");
        _windowgame.HandleMessage(message);
        
    }
    public void OnReceiveBreedDescriptionMessage(BreedDescriptionMessage message)
    {
        Debug.Log("��������� � ������� ���� + �����������");
        _windowDescription.RefreshInfo(message);
    } 
} 