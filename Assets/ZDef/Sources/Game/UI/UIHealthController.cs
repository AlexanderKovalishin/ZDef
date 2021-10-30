using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;

namespace ZDef.Game.UI
{
    public class UIHealthController : MonoBehaviour
    {
        [SerializeField] private UIHealthView _healthView;
        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<PlayerUpdateHealthEvent>(PlayerUpdateHealthEventListener);
        }

        private void PlayerUpdateHealthEventListener(PlayerUpdateHealthEvent args)
        {
            _healthView.SetValue(args.Health);
        }
    }
}