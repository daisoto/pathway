using System;
using UnityEngine;
using Zenject;

namespace Gameplay
{
public class GameplayController: IInitializable, IDisposable 
{
    private readonly SignalBus _signalBus;

    public GameplayController(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<ResumeGameSignal>(ResumeGame);
        _signalBus.Subscribe<PauseGameSignal>(PauseGame);
        _signalBus.Subscribe<QuitGameSignal>(QuitGame);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<ResumeGameSignal>(ResumeGame);
        _signalBus.Unsubscribe<PauseGameSignal>(PauseGame);
        _signalBus.Unsubscribe<QuitGameSignal>(QuitGame);
    }
    private void PauseGame() => Time.timeScale = 0;
    
    private void ResumeGame() => Time.timeScale = 1;
    
    private void QuitGame() => Application.Quit();
}
}