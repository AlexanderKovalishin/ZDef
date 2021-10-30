namespace ZDef.Game.BusEvents
{
    public readonly struct PlayerMoveEvent
    {
        public PlayerMoveEvent(float direction)
        {
            Direction = direction;
        }

        public float Direction { get; }
    }
}