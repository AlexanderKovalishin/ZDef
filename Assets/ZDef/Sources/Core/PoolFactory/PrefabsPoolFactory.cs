using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ZDef.Core.PoolFactory
{
    public class PrefabsFactory<TArgs, TResult> : MonoBehaviour 
        where TResult: MonoBehaviour, IReturnToPoolCallback<TResult>, IFactoryInit<TArgs>
    {
        [SerializeField] private TResult _prefab;
        [SerializeField] private int _startCapacity = 10;
        [SerializeField] private Transform _root;

        private readonly Queue<TResult> _instances = new Queue<TResult>();

        public TResult Instantiate(TArgs args)
        {
            TResult instance = DequeueOrCreateInstance();
            instance.transform.LocalIdentity();
            instance.gameObject.SetActive(true);
            instance.Init(args);
            instance.ReturnToPool += InstanceOnReturnToPool;
            return instance;
        }
        
        private TResult CreateInstance()
        {
            _prefab.gameObject.SetActive(false);
            // todo: pre awake injections here
            return Instantiate(_prefab, _root);
        }

        private TResult DequeueOrCreateInstance()
        {
            return _instances.Count == 0 
                ? CreateInstance() 
                : _instances.Dequeue();
        }
        
        private void Awake()
        {
            for (var i = 0; i < _startCapacity; i++)
            {
                _instances.Enqueue(CreateInstance());
            }
        }
        
        private void InstanceOnReturnToPool(TResult sender)
        {
            sender.gameObject.SetActive(false);
            _instances.Enqueue(sender);
        }
    }
}
