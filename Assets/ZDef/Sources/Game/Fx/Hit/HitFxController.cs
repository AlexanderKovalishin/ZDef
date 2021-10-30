using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;

namespace ZDef.Game.Fx
{
    public class HitFxController: MonoBehaviour
    {
        [SerializeField] private HitFxFactory _hitFxFactory;
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
            _hitFxFactory.Instantiate(new HitFxInitArgs(args.Position, args.Source));
        }
    }
}