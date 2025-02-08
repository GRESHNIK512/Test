using UnityEngine;
using Zenject;

public class HeadInstaller : MonoInstaller
{
    [SerializeField] private BreedService _breedService;
    [SerializeField] private WeatherService _weatherService; 

    //NW
    [Space(10)]
    [SerializeField] private MsgService _msgService;

    public override void InstallBindings()
    {
        Container.Bind<BreedService>().FromInstance(_breedService).AsSingle().NonLazy();
        Container.Bind<WeatherService>().FromInstance(_weatherService).AsSingle().NonLazy(); 

        Container.Bind<MsgService>().FromInstance(_msgService).AsSingle().NonLazy();  
    }
}