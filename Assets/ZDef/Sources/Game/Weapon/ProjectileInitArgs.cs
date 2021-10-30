using UnityEngine;

namespace ZDef.Game.Weapon
{
    public readonly struct ProjectileInitArgs
    {
        public Transform StartPosition { get; }
        public IProjectileTarget Target { get; }
        public int Damage { get; }
        public float Velocity{ get; }

        public ProjectileInitArgs(Transform startPosition, IProjectileTarget target, int damage, float velocity)
        {
            Target = target;
            Velocity = velocity;
            Damage = damage;
            StartPosition = startPosition;
        }
    }
}