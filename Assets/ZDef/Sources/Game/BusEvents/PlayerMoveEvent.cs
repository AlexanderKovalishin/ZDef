namespace ZDef.Game.BusEvents
{
    public readonly struct PlayerMoveEvent
    {
        public string PlayerId { get; }
        public float Direction { get; }

        public PlayerMoveEvent(string playerId, float direction)
        {
            PlayerId = playerId;
            Direction = direction;
        }
    }
}