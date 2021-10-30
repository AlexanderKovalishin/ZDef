using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Data;

namespace ZDef.Game.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private AnimatorSpeedByVelocity _speedByVelocity;
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _playerMove;
        [SerializeField] private float _velocityK = 20f;
        
        private EventBus _eventBus;
        private float _moveDirection;
        private float _currentVelocity;
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
            _eventBus.Subscribe<PlayerMoveEvent>(PlayerMoveEventListener);
            _eventBus.Subscribe<DefeatEvent>(DefeatEvent);
            _eventBus.Subscribe<VictoryEvent>(VictoryEventListener);
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<PlayerMoveEvent>(PlayerMoveEventListener);
            _eventBus.UnSubscribe<DefeatEvent>(DefeatEvent);
            _eventBus.UnSubscribe<VictoryEvent>(VictoryEventListener);
        }

        private void VictoryEventListener(VictoryEvent args)
        {
            enabled = false;
        }

        private void DefeatEvent(DefeatEvent args)
        {
            enabled = false;
        }
        
        private void PlayerMoveEventListener(PlayerMoveEvent args)
        {
            _moveDirection = args.Direction;
        }

        private void Update()
        {
            Vector3 position = _playerMove.position;
            float velocity = _moveDirection * _playerConfig.Velocity;
            _currentVelocity = Mathf.Lerp(_currentVelocity, velocity, Time.deltaTime * _velocityK);
            _animator.SetRun(Mathf.Abs(velocity) > 0.01f);
            _speedByVelocity.SetVelocity(velocity);

            float newPosition = ClampPosition(position.x + _currentVelocity * Time.deltaTime);
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