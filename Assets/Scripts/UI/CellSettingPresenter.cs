using System;
using System.Collections.Generic;
using Data;
using Gameplay;
using UnityEngine;
using Zenject;

namespace UI
{
public class CellSettingPresenter : Presenter<CellSettingView>, IInitializable, IDisposable
{
    private readonly CellsSettings _cellsSettings; 
    private readonly CameraOperator _cameraOperator;
    private readonly SignalBus _signalBus;
    
    public CellSettingPresenter(CellSettingView view, 
        CellsSettings cellsSettings, CameraOperator cameraOperator, 
        SignalBus signalBus) : base(view)
    {
        _cellsSettings = cellsSettings;
        _cameraOperator = cameraOperator;
        _signalBus = signalBus;
    }

    public void Initialize() 
    {
        Draw();
        _signalBus.Subscribe<StartGameSignal>(_view.Show);
        _signalBus.Subscribe<ResetGameSignal>(_view.Close);
    }

    public void Dispose() 
    {
        _signalBus.Unsubscribe<StartGameSignal>(_view.Show);
        _signalBus.Unsubscribe<ResetGameSignal>(_view.Close);
    }
    
    private void Draw()
    {
        var directions = EnumUtils.GetValues<Direction>();
        var viewModels = new List<CellSetterViewModel>();
        foreach (var dir in directions)
        {
            viewModels.Add(
                new CellSetterViewModel(
                    _cellsSettings.GetSprite(dir), 
                    _cellsSettings.GetRotation(dir), OnEndDrag));
            
            void OnEndDrag(Vector2 pos) => SetCell(dir, pos);
        }
        
        _view.Draw(viewModels);
    }
    
    private void SetCell(Direction dir, Vector2 pos)
    {
        var hits = _cameraOperator.Raycast(pos);
        
        foreach (var hit in hits)
            if (hit.collider.TryGetComponent(out CellBehaviour cell))
                cell.SetDirection(dir);
    }
}
}