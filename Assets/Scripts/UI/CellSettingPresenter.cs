using System.Collections.Generic;
using Data;
using Gameplay;
using UnityEngine;

namespace UI
{
public class CellSettingPresenter : Presenter<CellSettingView>
{
    private readonly CellsSettings _cellsSettings; 
    private readonly CameraOperator _cameraOperator;
    
    public CellSettingPresenter(CellSettingView view, 
        CellsSettings cellsSettings, CameraOperator cameraOperator) : base(view)
    {
        _cellsSettings = cellsSettings;
        _cameraOperator = cameraOperator;
        
        Draw();
    }
    
    public void Draw()
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