using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MsgService : MonoBehaviour
{
    private WindowDescription _windowDescription;
    private WindowGame _windowGame;  

    private Queue<(INetworkSender sender, Type expectedMessageType)> _messageQueue = new();
    private bool _isProcessing = false;

    private Coroutine _repeatCoroutine;
    private int _msgUniqueId = 0;

    [Inject]
    public void Construct(WindowGame windowGame, WindowDescription windowDescription)
    {
        _windowGame = windowGame;
        _windowDescription = windowDescription;
    }

    #region RegisterMsg
    public void RegistersMsg()
    {
        NetworkClient.RegisterHandler<WeatherMessage>(OnReceiveWeatherMessage);
        NetworkClient.RegisterHandler<BreedDataMessage>(OnReceiveBreedDataMessage);
        NetworkClient.RegisterHandler<BreedDescriptionMessage>(OnReceiveBreedDescriptionMessage);
    }
    #endregion 

    private void TrySendNextMessage()
    {
        if (!_isProcessing && _messageQueue.Count > 0)
        {
            _isProcessing = true;
            Debug.Log($"Отправка сообщения на Сервер");
            _messageQueue.Peek().sender.Send();
        }
    }

    private void CheckQueue(Type tryAddType)
    {
        if (_messageQueue.Count > 0 && _isProcessing && (_messageQueue.Peek().expectedMessageType != tryAddType ||
            _messageQueue.Peek().expectedMessageType == tryAddType && tryAddType == typeof(BreedDescriptionMessage)))
        {
            _messageQueue.Clear();
            _isProcessing = false;
        }
    } 
    
    public void SendToClientButtonClickMsg(int id)
    {
        Debug.Log($"Добавили в очередь клик по Кнопке id = {id} | MsgCount= {_messageQueue.Count + 1}");
        var type = id == 0 ? typeof(WeatherMessage) : typeof(BreedDataMessage);

        CheckQueue(type);
        _messageQueue.Enqueue((new ButtonClickMessage() { Id = id }, type));
        TrySendNextMessage();
    }

    public void SendToClientFactButtonClickMsg(string id)
    {
        Debug.Log($"Добавили в очередь клик по Fact id = {id}| MsgCount = {_messageQueue.Count + 1}");
        var type = typeof(BreedDescriptionMessage);

        CheckQueue(type);
        _messageQueue.Enqueue((new ButtonClickFactMessage() { Id = id, UnqIdMsg = GetUnqIdForMessage() }, type));
        TrySendNextMessage();
    } 
    
    public void OnReceiveWeatherMessage(WeatherMessage message)
    {
        Debug.Log("Сообщение с сервера Погода");
        HandleMessage(message, msg => _windowGame.HandleMessage(msg));
    }

    public void OnReceiveBreedDataMessage(BreedDataMessage message)
    {
        Debug.Log("Сообщение с сервера общий список фактов");
        HandleMessage(message, msg => _windowGame.HandleMessage(msg));
    }

    public void OnReceiveBreedDescriptionMessage(BreedDescriptionMessage message)
    {
        Debug.Log("Сообщение с сервера факт + подробности");
        HandleMessage(message, msg => _windowDescription.RefreshInfo(msg), message.UnqIdMsg);
    }

    private void HandleMessage<T>(T message, Action<T> specificHandler, int unqID = -1)
    {

        if (!IsExpectedMessageType<T>())
        {
            Debug.LogWarning("Сообщение игнорируется: не совпадает с ожидаемым типом.");
            return;
        }

        if (unqID != -1)
        {
            if (_messageQueue.Peek().sender is ButtonClickFactMessage factMessage)
            {
                if (factMessage.UnqIdMsg != unqID)
                {
                    Debug.LogWarning("Сообщение игнорируется: не совпадает UniqIdMsg.");
                    return;
                }
            }
        }

        ProcessNextMessage();
        specificHandler(message);
    }

    private void ProcessNextMessage()
    {
        _messageQueue.Dequeue();
        _isProcessing = false;
        TrySendNextMessage();
    }

    public void CheckRepeatButton(int id)
    {
        SendToClientButtonClickMsg(id);

        if (id == 0)
        {
            _repeatCoroutine = StartCoroutine(RepeatSendMessage(id));
        }
        else if (_repeatCoroutine != null)
        {
            StopCoroutine(_repeatCoroutine);
            _repeatCoroutine = null;

        }
    }

    private IEnumerator RepeatSendMessage(int id)
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            SendToClientButtonClickMsg(id);
        }
    }

    private int GetUnqIdForMessage()
    {
        if (_msgUniqueId >= int.MaxValue - 1) _msgUniqueId = 0;
        return _msgUniqueId++;
    }

    private bool IsExpectedMessageType<T>()
    {
        return _messageQueue.Count > 0 && _messageQueue.Peek().expectedMessageType == typeof(T);
    }
}