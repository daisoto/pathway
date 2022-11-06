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
        transform.position = position;
        
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
            .DOMove(position, _timeToMove)
            .OnComplete(() => hasFinished = true);
        
        await UniTask.WaitUntil(() => hasFinished, 
            PlayerLoopTiming.LastUpdate, _cts.Token);
    }
}
}