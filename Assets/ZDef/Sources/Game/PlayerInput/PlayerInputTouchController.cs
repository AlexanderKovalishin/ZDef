using UnityEngine;
using UnityEngine.EventSystems;
using ZDef.GameNetwork;
using ZDef.GameNetwork.Events;
using Zenject;

namespace ZDef.Game.PlayerInput
{
    public class PlayerInputTouchController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] private GameNetworkEventBus _networkEventBus;
        [Inject] private GameNetworkApiClient _networkClient;
        
        private Vector2 _direction;
        private Vector2 _pressPosition;
        private int _pressId;
        private string _playerId;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _pressId = eventData.pointerId;
            _pressPosition = eventData.position;
            _direction = Vector2.zero;

            _playerId = _networkClient.CurrentPlayerActorNumber().ToString();
            
            _networkEventBus.Send(new MovePlayerNetworkEvent(_playerId, _direction));
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_pressId != eventData.pointerId) return;
            var delta = eventData.position - _pressPosition;
            _direction = new Vector2(
                Mathf.Clamp(delta.x * 4 / Screen.width, -1, 1),
                Mathf.Clamp(delta.y * 4 / Screen.height, -1, 1));
            _networkEventBus.Send(new MovePlayerNetworkEvent(_playerId, _direction));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_pressId != eventData.pointerId) return;
            _direction = Vector2.zero;
            _networkEventBus.Send(new MovePlayerNetworkEvent(_playerId, _direction));
        }
    }
}