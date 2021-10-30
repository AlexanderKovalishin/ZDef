namespace ZDef.Game.BusEvents
{
    public readonly struct PlayerUpdateHealthEvent
    {
        public int Health { get; }
        
        public PlayerUpdateHealthEvent(int health)
        {
            Health = health;
        }
    }
}