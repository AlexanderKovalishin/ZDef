using UnityEngine;

namespace ZDef.Game.BusEvents
{
    public interface IHitEvent
    {
        public Vector3 Position { get; }
        public Vector3 Source { get; }
    }
}