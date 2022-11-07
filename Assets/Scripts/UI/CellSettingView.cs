using System.Collections.Generic;
using UnityEngine;

namespace UI
{
public class CellSettingView: View
{
    [SerializeField]
    private CellSetter _cellSetterPrefab;
    
    [SerializeField]
    private RectTransform _cellSettersContainer;
    
    public void Draw(IList<CellSetterViewModel> viewModels)
    {
        foreach (var vm in viewModels)
        {
            var setter = Instantiate(_cellSetterPrefab, _cellSettersContainer)
                .SetSprite(vm.Sprite)
                .SetRotation(vm.Rotation)
                .SetOnEndDrag(vm.OnEndDrag);
        }
    }
}
}