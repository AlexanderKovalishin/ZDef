using ZDef.Game.Enemies;

namespace ZDef.Game.BusEvents
{
    public readonly struct InstantiateEnemyEvent
    {
        public EnemyController EnemyController { get; }
        public InstantiateEnemyEvent(EnemyController enemyController)
        {
            EnemyController = enemyController;
        }
    }
}