using System.Collections;
using DG.Tweening;
using UnityEngine;
using ZDef.Core.PoolFactory;

namespace ZDef.Game.Fx.Decals
{
    public class Decal : MonoBehaviour, IReturnToPoolCallback<Decal>, IFactoryInit<DecalsInitArgs>
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _hideDuration = 1f;
        [SerializeField] private float _showDuration = 0.5f;

        private float _blend;
        
        public event DestroyDelegate<Decal> ReturnToPool;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _blend = _sprite.color.a;
        }

        public void Init(DecalsInitArgs args)
        {
            _sprite.DOFade(_blend, _showDuration);
            _transform.position = args.Position;
            StartCoroutine(ReturnAfterDelay(_duration));
        }

        private IEnumerator ReturnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _sprite.DOFade(0, _hideDuration);
            yield return new WaitForSeconds(_hideDuration);
            _sprite.DOKill();
            ReturnToPool?.Invoke(this);
        }

        private void OnDestroy()
        {
            _sprite.DOKill();
        }
    }
}