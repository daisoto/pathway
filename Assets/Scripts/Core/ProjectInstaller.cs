using System;
using System.Linq;
using Data;
using Gameplay;
using UI;
using UnityEngine;
using Zenject;

public interface ISignal { }

namespace Core
{
public class ProjectInstaller: MonoInstaller
{
    [SerializeField]
    private GridSettings _gridSettings;
    
    [SerializeField]
    private CellsSettings _cellsSettings;
    
    [SerializeField]
    private MoversSettings _moversSettings;
    
    [SerializeField]
    private ScoresSettings _scoresSettings;
    
    public override void InstallBindings()
    {
        BindSignals();
        BindSettings();
        BindGameplay();
        BindUI();
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
    
    private void BindSettings()
    {
        Container.BindInterfacesAndSelfTo<GridSettings>()
            .FromInstance(_gridSettings);
        
        Container.BindInterfacesAndSelfTo<CellsSettings>()
            .FromInstance(_cellsSettings);
        
        Container.BindInterfacesAndSelfTo<MoversSettings>()
            .FromInstance(_moversSettings);
        
        Container.BindInterfacesAndSelfTo<ScoresSettings>()
            .FromInstance(_scoresSettings);
    }
    
    private void BindGameplay()
    {
        Container.BindInterfacesAndSelfTo<GridController>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<MoversManager>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<LevelController>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ScoreManager>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameplayController>()
            .AsSingle()
            .NonLazy();
    }
    
    private void BindUI()
    {
        Container.BindInterfacesAndSelfTo<GameFlowPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<CellSettingPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ScorePresenter>()
            .AsSingle()
            .NonLazy();
    }
}
}