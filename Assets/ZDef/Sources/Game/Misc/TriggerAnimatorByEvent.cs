using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;

namespace ZDef.Game.Misc
{
    public class TriggerAnimatorByEvent<TEvent> : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _triggerName = "Run";

        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<TEvent>(PlayerHitEventListener);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<TEvent>(PlayerHitEventListener);
        }
        
        private void PlayerHitEventListener(TEvent args)
        {
            _animator.SetTrigger(_triggerName);
        }
    }
}