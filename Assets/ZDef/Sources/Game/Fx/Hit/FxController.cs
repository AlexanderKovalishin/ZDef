using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;

namespace ZDef.Game.Fx.Hit
{
    public class FxController<TEvent>: MonoBehaviour
        where TEvent: IHitEvent 
    {
        [SerializeField] private HitFxFactory _hitFxFactory;
        private EventBus _eventBus;
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<TEvent>(HitFxEventListener);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<TEvent>(HitFxEventListener);
        }

        private void HitFxEventListener(TEvent args)
        {
            _hitFxFactory.Instantiate(new HitFxInitArgs(args.Position, args.Source));
        }
    }
}