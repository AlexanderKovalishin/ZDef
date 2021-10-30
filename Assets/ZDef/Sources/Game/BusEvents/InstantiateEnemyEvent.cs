using ZDef.Game.Enemies;

namespace ZDef.Game.BusEvents
{
    public readonly struct InstantiateEnemyEvent
    {
        public EnemyController EnemyController { get; }
        public EnemyControllerInitArgs Args { get; }
        
        public InstantiateEnemyEvent(EnemyController enemyController, EnemyControllerInitArgs args)
        {
            EnemyController = enemyController;
            Args = args;
        }
    }
}