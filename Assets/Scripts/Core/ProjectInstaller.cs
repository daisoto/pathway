using System;
using System.Linq;
using Data;
using Gameplay;
using UnityEngine;
using Zenject;

public interface ISignal { }

namespace Core
{
public class ProjectInstaller: MonoInstaller
{
    [SerializeField]
    private GridSettings _gridSettings;

    public override void InstallBindings()
    {
        BindSignals();
        
        Container.BindInterfacesAndSelfTo<GridSettings>()
            .FromInstance(_gridSettings);

        Container.BindInterfacesAndSelfTo<GridController>()
            .AsSingle()
            .NonLazy();
    }
    
    private void BindSignals()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(ISignal).IsAssignableFrom(p)
                        && !p.IsInterface
                        && !p.IsAbstract);                        
                        
        foreach (var type in types) 
            Container.DeclareSignal(type);

        SignalBusInstaller.Install(Container);
    }
}
}