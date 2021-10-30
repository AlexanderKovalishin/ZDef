using UnityEngine;

namespace ZDef.Game.Enemies
{
    public readonly struct EnemyControllerInitArgs
    {
        public Vector3 StartPosition { get; }
        public int Health { get; }
        public float Velocity { get; }
        public float DeadLine { get; }
        public int Damage { get; }

        public EnemyControllerInitArgs(Vector3 startPosition, int health, int damage, float velocity, float deadLine)
        {
            StartPosition = startPosition;
            Velocity = velocity;
            DeadLine = deadLine;
            Damage = damage;
            Health = health;
        }

    }
}