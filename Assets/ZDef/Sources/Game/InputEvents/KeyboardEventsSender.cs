using System;
using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;
using ZDef.Game.Data;

namespace ZDef.Game.InputEvents
{
    public class KeyboardEventsSender: MonoBehaviour
    {
        [SerializeField] private KeyboardConfig _keyboardConfig;

        private EventBus _eventBus;
        private void Awake()
        {
            _eventBus = ServiceLocator.Locate<EventBus>();
        }

        private void Update()
        {
            var direction = 0f;
            foreach (KeyCode keyCode in _keyboardConfig.MoveLeft)
            {
                if (!Input.GetKey(keyCode)) continue;
                direction -= 1;
                break;
            }
            foreach (KeyCode keyCode in _keyboardConfig.MoveRight)
            {
                if (!Input.GetKey(keyCode)) continue;
                direction += 1;
                break;
            }
            _eventBus.Send(new PlayerMoveEvent(direction));
        }
    }
}