using UnityEngine;

namespace ZDef.Game.Enemies
{
    public readonly struct EnemyControllerInitArgs
    {
        public Vector3 StartPosition { get; }
        public int Health { get; }
        public float Velocity { get; }
        public float DeadLine { get; }

        public EnemyControllerInitArgs(Vector3 startPosition, int health, float velocity, float deadLine)
        {
            StartPosition = startPosition;
            Velocity = velocity;
            DeadLine = deadLine;
            Health = health;
        }

    }
}