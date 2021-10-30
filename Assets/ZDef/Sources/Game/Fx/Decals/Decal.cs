using System.Collections;
using UnityEngine;
using ZDef.Core.PoolFactory;

namespace ZDef.Game.Fx.Decals
{
    public class Decal : MonoBehaviour, IReturnToPoolCallback<Decal>, IFactoryInit<DecalsInitArgs>
    {
        [SerializeField] private float _duration = 1f;

        public event DestroyDelegate<Decal> ReturnToPool;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Init(DecalsInitArgs args)
        {
            _transform.position = args.Position;
            StartCoroutine(ReturnAfterDelay(_duration));
        }

        private IEnumerator ReturnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnToPool?.Invoke(this);
        }
    }
}