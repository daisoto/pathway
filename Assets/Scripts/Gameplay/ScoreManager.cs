using UniRx;
using System;
using Data;
using Zenject;

namespace Gameplay
{
public class ScoreManager: IInitializable, IDisposable
{
    public IReadOnlyReactiveProperty<int> Scores => _scores;
    private readonly ReactiveProperty<int> _scores;

    private readonly ScoresSettings _settings;
    private readonly SignalBus _signalBus;

    public ScoreManager(ScoresSettings settings, SignalBus signalBus)
    {
        _settings = settings;
        _signalBus = signalBus;
        
        _scores = new ReactiveProperty<int>(0);
    }

    public void Initialize() => 
        _signalBus.Subscribe<VictorySignal>(SetLevelScore);

    public void Dispose() => 
        _signalBus.Unsubscribe<VictorySignal>(SetLevelScore);
    
    private void SetLevelScore() => 
        _scores.Value += _settings.ScoresForLevel;
}
}