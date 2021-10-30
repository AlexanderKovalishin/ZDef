using UnityEngine;

namespace ZDef.Game.BusEvents
{
    public readonly struct HitFxEvent
    {
        public Vector3 Position { get; }
        public Vector3 Source { get; }
        public int Damage { get; }
        public HitFxEvent(Vector3 position, Vector3 source, int damage)
        {
            Position = position;
            Source = source;
            Damage = damage;
        }
    }
}