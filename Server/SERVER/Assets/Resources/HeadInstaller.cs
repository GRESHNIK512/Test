using UnityEngine;
using Zenject;

public class HeadInstaller : MonoInstaller
{
    //NW
    [SerializeField] private MsgService _msgService;

    public override void InstallBindings()
    {  
        Container.Bind<MsgService>().FromInstance(_msgService).AsSingle().NonLazy();  
    }
}