using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Data;

namespace ZDef.Game.UI
{
    public class UIHealthController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private UIHealthView[] _healthViews;
        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<PlayerUpdateHealthEvent>(PlayerUpdateHealthEventListener);
        }

        private void PlayerUpdateHealthEventListener(PlayerUpdateHealthEvent args)
        {
            foreach (UIHealthView healthView in _healthViews)
            {
                healthView.SetValue(args.Health, _playerConfig.Health);
            }
        }
    }
}