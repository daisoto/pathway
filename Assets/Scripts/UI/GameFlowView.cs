using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
public class GameFlowView: View
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Button _resumeButton;
    [SerializeField]
    private Button _resetButton;
    [SerializeField]
    private Button _exitButton;
    
    public GameFlowView SetOnStart(Action onStart)
    {
        _startButton.onClick.AddListener(onStart.Invoke);
        
        return this;
    }
    
    public GameFlowView SetOnPause(Action onPause)
    {
        _pauseButton.onClick.AddListener(onPause.Invoke);
        
        return this;
    }
    
    public GameFlowView SetOnResume(Action onContinue)
    {
        _resumeButton.onClick.AddListener(onContinue.Invoke);
        
        return this;
    }
    
    public GameFlowView SetOnReset(Action onReset)
    {
        _resetButton.onClick.AddListener(onReset.Invoke);
        
        return this;
    }
    
    public GameFlowView SetOnExit(Action onExit)
    {
        _exitButton.onClick.AddListener(onExit.Invoke);
        
        return this;
    }
    
    public GameFlowView SetStart(bool flag)
    {
        _startButton.gameObject.SetActive(flag);
        
        return this;
    }
    
    public GameFlowView SetPause(bool flag) 
    { 
        _pauseButton.gameObject.SetActive(flag);
        
        return this;
    }
    
    public GameFlowView SetResume(bool flag) 
    {
        _resumeButton.gameObject.SetActive(flag);
        
        return this;
    }
        
    public GameFlowView SetReset(bool flag)
    {
        _resetButton.gameObject.SetActive(flag);
        
        return this;
    }
}
}