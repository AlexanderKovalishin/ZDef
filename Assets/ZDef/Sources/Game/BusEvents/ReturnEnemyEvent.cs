using ZDef.Game.Enemies;

namespace ZDef.Game.BusEvents
{
    public readonly struct ReturnEnemyEvent
    {
        public ReturnEnemyEvent(EnemyController enemyController)
        {
            EnemyController = enemyController;
        }

        public EnemyController EnemyController { get; }
    }
}