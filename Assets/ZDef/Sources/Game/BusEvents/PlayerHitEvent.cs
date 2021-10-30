using Vector3 = UnityEngine.Vector3;

namespace ZDef.Game.BusEvents
{
    public readonly struct PlayerHitEvent: IDecalEvent, IHitEvent
    {
        public int Damage { get; }
        public Vector3 Position { get; }
        public Vector3 Source { get; }

        public PlayerHitEvent(int damage, Vector3 position, Vector3 source)
        {
            Damage = damage;
            Position = position;
            Source = source;
        }
    }
}