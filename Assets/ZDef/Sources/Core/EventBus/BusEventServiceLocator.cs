using System;
using System.Collections.Generic;

namespace ZDef.Core.EventBus
{
    internal class BusEventServiceLocator
    {
        private readonly Dictionary<Type, object> _busEvents = new Dictionary<Type, object>();

        public BusEvent<TArgs> Locate<TArgs>()
        {
            Type type = typeof(TArgs);
            if (!_busEvents.ContainsKey(type))
                _busEvents.Add(type, new BusEvent<TArgs>());
            return (BusEvent<TArgs>)_busEvents[type];
        }
    }
}