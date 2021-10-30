using System;
using System.Collections.Generic;

namespace ZDef.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();
        
        public static T Locate<T>() where T : new()
        {
            Type type = typeof(T);
            if (!Services.ContainsKey(type))
                Services.Add(type, new T());
            return (T)Services[type];
        }
    }
}