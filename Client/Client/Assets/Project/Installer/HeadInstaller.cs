using UnityEngine;
using Zenject;

public class HeadInstaller : MonoInstaller
{
    //UI
    [SerializeField] private Screen _windowService;
    [SerializeField] private WindowDescription _windowDescription;
    [SerializeField] private WindowGame _windowGame;
    [SerializeField] private FactsContent _factsContent;

    //NW
    [Space(10)]
    [SerializeField] private MsgService _msgService;

    //Sobj
    [SerializeField] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
        Container.Bind<Screen>().FromInstance(_windowService).AsSingle().NonLazy();
        Container.Bind<WindowGame>().FromInstance(_windowGame).AsSingle().NonLazy();
        Container.Bind<WindowDescription>().FromInstance(_windowDescription).AsSingle().NonLazy();
        Container.Bind<FactsContent>().FromInstance(_factsContent).AsSingle().NonLazy();

        Container.Bind<MsgService>().FromInstance(_msgService).AsSingle().NonLazy();

        Container.BindInstance(_gameSettings).AsSingle();
        //Container.QueueForInject(_windowService);
    }
}