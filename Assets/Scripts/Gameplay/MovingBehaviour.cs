using System.Threading;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
public class MovingBehaviour: MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    
    public MovingBehaviour SetColor(Color color)
    {
        _renderer.material.color = color;
            
        return this;
    }
    
    public async UniTask Move(Vector3 position, float time, 
        CancellationToken token)
    {
        var hasFinished = false;
        transform
            .DOMove(position, time)
            .OnComplete(() => hasFinished = true);
        
        await UniTask.WaitUntil(
            () => hasFinished, 
            PlayerLoopTiming.LastUpdate, 
            token);
    }
}
}