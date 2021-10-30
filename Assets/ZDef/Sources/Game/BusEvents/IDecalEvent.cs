using UnityEngine;

namespace ZDef.Game.BusEvents
{
    public interface IHitEvent
    {
        public Vector3 Position { get; }
        public Vector3 Source { get; }
    }

    public interface IDecalEvent
    {
        public Vector3 Position { get; }
    }
}