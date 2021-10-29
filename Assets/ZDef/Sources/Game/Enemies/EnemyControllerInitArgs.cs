using UnityEngine;

namespace ZDef.Game.Enemies
{
    public readonly struct EnemyControllerInitArgs
    {
        public Transform StartPosition { get; }
        public float Velocity { get; }

        public EnemyControllerInitArgs(Transform startPosition, float velocity)
        {
            StartPosition = startPosition;
            Velocity = velocity;
        }

    }
}