using Data;
using Gameplay;
using UnityEngine;
using Zenject;

namespace Core
{
public class ProjectInstaller: MonoInstaller
{
    [SerializeField]
    private GridSettings _gridSettings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GridSettings>()
            .FromInstance(_gridSettings);

        Container.BindInterfacesAndSelfTo<GridController>()
            .AsSingle()
            .NonLazy();
    }
}
}