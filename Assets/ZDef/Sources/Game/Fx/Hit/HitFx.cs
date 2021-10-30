using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using ZDef.Core.PoolFactory;

namespace ZDef.Game.Fx.Hit
{
    public class HitFx : MonoBehaviour, IReturnToPoolCallback<HitFx>, IFactoryInit<HitFxInitArgs>
    {
        [SerializeField] private ParticleSystem _fx;
        [SerializeField] private float _duration = 1f;
        
        public event DestroyDelegate<HitFx> ReturnToPool;

        private Transform _transform;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void Init(HitFxInitArgs args)
        {
            Vector3 direction = args.Position - args.Source;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transform.rotation = quaternion.Euler(0, 0, rotation);
            _transform.position = args.Position;
            _fx.Play();
            StartCoroutine(ReturnAfterDelay(_duration));
        }

        private IEnumerator ReturnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _fx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ReturnToPool?.Invoke(this);
        }
    }
}