using System;
using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Data;

namespace ZDef.Game.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _playerMove;
        private EventBus _eventBus;
        private float _moveDirection;
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<PlayerMoveEvent>(PlayerMoveEventListener);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<PlayerMoveEvent>(PlayerMoveEventListener);
        }

        private void PlayerMoveEventListener(PlayerMoveEvent args)
        {
            _moveDirection = args.Direction;
        }

        private void Update()
        {
            Vector3 position = _playerMove.position;
            float newPosition = ClampPosition(position.x + _moveDirection * Time.deltaTime * _playerConfig.Velocity);
            position = new Vector3(newPosition, position.y, position.z);
            _playerMove.position = position;
        }

        private float ClampPosition(float value)
        {
            if (value < _min.transform.position.x)
            {
                value = _min.transform.position.x;
            }
            if (value > _max.transform.position.x)
            {
                value = _max.transform.position.x;
            }

            return value;
        }
    }
}