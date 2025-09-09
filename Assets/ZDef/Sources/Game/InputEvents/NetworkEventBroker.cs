using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.GameNetwork;
using ZDef.GameNetwork.Events;
using Zenject;

namespace ZDef.Game.InputEvents
{
    public class NetworkEventBroker : MonoBehaviour
    {
        [Inject] private GameNetworkEventBus _networkEventBus;
        private EventBus _eventBus;
        
        private void Awake()
        {
            _networkEventBus.Subscribe<MovePlayerNetworkEvent>(MovePlayerNetworkEventListener);
            _eventBus = ServiceLocator.Locate<EventBus>();
        }

        private void OnDestroy()
        {
            _networkEventBus.UnSubscribe<MovePlayerNetworkEvent>(MovePlayerNetworkEventListener);
        }

        private void MovePlayerNetworkEventListener(MovePlayerNetworkEvent eventData)
        {
            Debug.Log($"MovePlayerNetworkEvent: {eventData.PlayerId} {eventData.Direction}");
            _eventBus.Send(new PlayerMoveEvent(eventData.PlayerId, eventData.Direction.x));
        }
    }
}