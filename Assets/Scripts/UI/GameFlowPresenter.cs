using Gameplay;
using Zenject;

namespace UI
{
public class GameFlowPresenter: Presenter<GameFlowView>, IInitializable
{
    private readonly SignalBus _signalBus; 
    
    public GameFlowPresenter(GameFlowView view, SignalBus signalBus) : 
        base(view) => _signalBus = signalBus;

    public void Initialize()
    {
        _view
            .SetOnStart(StartGame)
            .SetOnResume(ResumeGame)
            .SetOnPause(PauseGame)
            .SetOnReset(Reset)
            .SetOnExit(Exit);
    }
    
    private void StartGame()
    {
        _signalBus.Fire(new StartGameSignal());
        _view
            .SetReset(true)
            .SetPause(true)
            .SetStart(false);
    }
    
    private void ResumeGame()
    {
        _signalBus.Fire(new ResumeGameSignal());
        _view
            .SetResume(false)
            .SetPause(true);
    }
    
    private void PauseGame()
    {
        _signalBus.Fire(new PauseGameSignal());
        _view
            .SetResume(true)
            .SetPause(false);
    }
    
    private void Reset() => _signalBus.Fire(new ResetGameSignal());
    private void Exit() => _signalBus.Fire(new QuitGameSignal());
}
}