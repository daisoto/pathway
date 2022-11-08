using System;
using UnityEngine;
using Zenject;

namespace Gameplay
{
public class GameplayController: IInitializable, IDisposable
{
    private readonly LevelController _levelController;
    private readonly SignalBus _signalBus; 

    public GameplayController(LevelController levelController, SignalBus signalBus)
    {
        _levelController = levelController;
        _signalBus = signalBus;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<StartGameSignal>(StartGame);
        _signalBus.Subscribe<ResetGameSignal>(Reset);
        _signalBus.Subscribe<ResumeGameSignal>(ResumeGame);
        _signalBus.Subscribe<PauseGameSignal>(PauseGame);
        _signalBus.Subscribe<QuitGameSignal>(QuitGame);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<StartGameSignal>(StartGame);
        _signalBus.Unsubscribe<ResetGameSignal>(Reset);
        _signalBus.Unsubscribe<ResumeGameSignal>(ResumeGame);
        _signalBus.Unsubscribe<PauseGameSignal>(PauseGame);
        _signalBus.Unsubscribe<QuitGameSignal>(QuitGame);
    }
    
    private void StartGame() => _levelController.StartLevel(); 
    
    private void Reset() => _levelController.Reset();

    private void PauseGame() => Time.timeScale = 0;
    
    private void ResumeGame() => Time.timeScale = 1;
    
    private void QuitGame() => Application.Quit();
}
}