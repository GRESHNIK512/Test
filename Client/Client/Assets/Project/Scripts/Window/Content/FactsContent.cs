using Mirror;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactsContent : Content
{
    [Inject] WindowGame _windowgame; 
    [SerializeField] private List<Fact> _facts; 

    public override void Refresh(NetworkMessage msg)
    {
        var Msg = (BreedDataMessage)msg ;

        for (int i = 0; i < _facts.Count; i++)
        {
            if (Msg.BreedsData.Length - 1 >= i)
            {
                _facts[i].SetInfo($"{i + 1} - {Msg.BreedsData[i].attributes.name}");
                _facts[i].Id = Msg.BreedsData[i].id;
            }
        }
        _windowgame.ShowMyContent(1);
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
} 