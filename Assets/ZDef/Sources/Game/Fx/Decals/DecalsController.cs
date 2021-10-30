using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;

namespace ZDef.Game.Fx.Decals
{
    public class DecalsController: MonoBehaviour
    {
        [SerializeField] private DecalsFactory[] _decalsFactories;
        private EventBus _eventBus;
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<HitFxEvent>(HitFxEventListener);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<HitFxEvent>(HitFxEventListener);
        }

        private void HitFxEventListener(HitFxEvent args)
        {
            DecalsFactory factory = _decalsFactories[Random.Range(0, _decalsFactories.Length - 1)];
            factory.Instantiate(new DecalsInitArgs(args.Position));
        }    
    }
}