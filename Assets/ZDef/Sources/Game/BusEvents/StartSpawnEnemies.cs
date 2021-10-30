namespace ZDef.Game.BusEvents
{
    public readonly struct StartSpawnEnemies
    {
        public int EnemiesCount { get; }
        public float MinSpawnTimeout { get; }
        public float MaxSpawnTimeout { get; }

        public StartSpawnEnemies(int enemiesCount, float minSpawnTimeout, float maxSpawnTimeout)
        {
            EnemiesCount = enemiesCount;
            MinSpawnTimeout = minSpawnTimeout;
            MaxSpawnTimeout = maxSpawnTimeout;
        }
    }
}