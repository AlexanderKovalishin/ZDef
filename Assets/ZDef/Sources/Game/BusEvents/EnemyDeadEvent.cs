using ZDef.Game.Enemies;

namespace ZDef.Game.BusEvents
{
    public readonly struct EnemyDeadEvent
    {
        public EnemyController Instance { get; }

        public EnemyDeadEvent(EnemyController instance)
        {
            Instance = instance;
        }
    }
}