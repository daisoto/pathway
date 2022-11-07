using System.Threading;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
public class MoverBehaviour: MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    
    private float _timeToMove;
    private CancellationTokenSource _cts;
    
    private Vector3 _verticalOffset
    {
        get
        {
            if (_verticalOffsetInternal == default)
                _verticalOffsetInternal = 
                    new Vector3(0, transform.localScale.y / 2, 0);
            
            return  _verticalOffsetInternal;
        }
    }
    
    private Vector3 _verticalOffsetInternal;
    
    public MoverBehaviour SetColor(Color color)
    {
        _renderer.material.color = color;
            
        return this;
    }
    
    public MoverBehaviour SetTimeToMove(float timeToMove)
    {
        _timeToMove = timeToMove;
            
        return this;
    }
    
    public MoverBehaviour SetPosition(Vector3 position)
    {
        transform.position = GetFixedPosition(position);
        
        return this;
    }
    
    public MoverBehaviour StopMoving()
    {
        _cts.Cancel();
        
        return this;
    }

    public async UniTask Move(Vector3 position)
    {
        _cts = new CancellationTokenSource();
        var hasFinished = false;
        transform
            .DOMove(GetFixedPosition(position), _timeToMove)
            .SetEase(Ease.Linear)
            .OnComplete(() => hasFinished = true);
        
        await UniTask.WaitUntil(() => hasFinished, 
            PlayerLoopTiming.LastUpdate, _cts.Token);
    }
    
    private Vector3 GetFixedPosition(Vector3 position) => 
        position + _verticalOffset;
}
}