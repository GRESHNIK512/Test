using Mirror;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactsContent : Content
{
    private WindowGame _windowGame;
    private MsgService _msgService;

    [SerializeField] private List<Fact> _facts;

    [Inject]
    public void Construct(WindowGame windowGame, MsgService msgService)
    {
        _windowGame = windowGame;
        _msgService = msgService;
    }

    public override void Refresh(NetworkMessage msg)
    {
        var Msg = (BreedDataMessage)msg;

        for (int i = 0; i < _facts.Count; i++)
        {
            if (Msg.BreedsData.Length - 1 >= i)
            {
                _facts[i].SetInfo($"{i + 1} - {Msg.BreedsData[i].Attributes.Name}");
                _facts[i].Id = Msg.BreedsData[i].Id;
            }
        }
        _windowGame.ShowMyContent(1);
    }

    public void StopAnim()
    {
        foreach (var fact in _facts)
        {
            fact.StopRotate();
        }
    }

    public void ResetAllFactIgnoreMe(Fact ignoreFact)
    {
        foreach (var fact in _facts)
        {
            if (fact.Equals(ignoreFact)) continue;
            fact.StopRotate();
        }
    }

    public void OnFactChosen(string factId)
    {
        _msgService.SendToClientFactButtonClickMsg(factId);
    }
}