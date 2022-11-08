using System;
using Gameplay;
using UniRx;
using Zenject;

namespace UI
{
public class ScorePresenter: Presenter<ScoreView>, IInitializable, IDisposable
{
    private readonly ScoreManager _scoreManager;
    private readonly SignalBus _signalBus;
    
    private IDisposable _subscription;
    
    public ScorePresenter(ScoreView view, 
        ScoreManager scoreManager, SignalBus signalBus) : base(view)
    {
        _scoreManager = scoreManager;
        _signalBus = signalBus;
    }
    
    public void Initialize()
    {
        _subscription = _scoreManager.Scores
            .Subscribe(score => _view.SetScore(score));
        _signalBus.Subscribe<StartGameSignal>(_view.Show);
        _signalBus.Subscribe<ResetGameSignal>(_view.Close);
    }

    public void Dispose()
    {
        _subscription.Dispose();
        _signalBus.Unsubscribe<StartGameSignal>(_view.Show);
        _signalBus.Unsubscribe<ResetGameSignal>(_view.Close);
    }
}
}