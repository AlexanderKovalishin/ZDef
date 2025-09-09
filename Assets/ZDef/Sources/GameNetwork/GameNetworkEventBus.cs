using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Client;
using Photon.Realtime;

namespace ZDef.GameNetwork
{
    [UsedImplicitly]
    public class GameNetworkEventBus
    {
        private readonly RealtimeClient _realtime;
        private readonly Dictionary<Type, GameNetworkEventContainer> _eventTypeMap = new ();
        private readonly Dictionary<byte, GameNetworkEventContainer> _eventCodeMap = new ();
        private byte _currentCode;

        private const byte MinCode = 1; 
        private const byte MaxCode = 199; 

        public GameNetworkEventBus(RealtimeClient realtime)
        {
            _realtime = realtime;
            _realtime.EventReceived += RealtimeOnEventReceived;
            _currentCode = MinCode;
        }

        public void Register<TEvent>(IGameNetworkEventSerializer<TEvent> serializer)
        {
            if (_currentCode > MaxCode)
            {
                throw new Exception("only 198 events allowed");
            }

            if (_eventTypeMap.ContainsKey(typeof(TEvent)))
            {
                throw new ArgumentException($"{typeof(TEvent).Name} register multiple times");
            }

            var container = new GameNetworkEventContainer<TEvent>(serializer, _currentCode);
            _eventCodeMap.Add(_currentCode, container); 
            _eventTypeMap.Add(typeof(TEvent), container); 
            
            _currentCode++;
        }

        private void RealtimeOnEventReceived(EventData eventData)
        {
            if (eventData.Code > MaxCode) return;
            _eventCodeMap[eventData.Code].DeserializeAndInvoke(eventData);
        }

        public void Subscribe<TEvent>(Action<TEvent> listener)
        {
            ((GameNetworkEventContainer<TEvent>)_eventTypeMap[typeof(TEvent)]).AddListener(listener);
        }
        
        public void UnSubscribe<TEvent>(Action<TEvent> listener)
        {
            ((GameNetworkEventContainer<TEvent>)_eventTypeMap[typeof(TEvent)]).RemoveListener(listener);
        }

        public void Send<TEvent>(TEvent eventData)
        {
            var container = ((GameNetworkEventContainer<TEvent>) _eventTypeMap[typeof(TEvent)]);
            var bytes = container.Serialize(eventData);
            _realtime.OpRaiseEvent(container.EventCode, bytes, RaiseEventArgs.Default, SendOptions.SendReliable);
        }
    }
}