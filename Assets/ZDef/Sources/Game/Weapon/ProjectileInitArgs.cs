using UnityEngine;

namespace ZDef.Game.Weapon
{
    public readonly struct ProjectileInitArgs
    {
        public Transform StartPosition { get; }
        public Transform Target { get; }
        public float Velocity{ get; }

        public ProjectileInitArgs( Transform startPosition, Transform target, float velocity)
        {
            Target = target;
            Velocity = velocity;
            StartPosition = startPosition;
        }
    }
}