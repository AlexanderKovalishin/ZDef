using UnityEngine;

namespace ZDef.Game.Weapon
{
    public interface IProjectileTarget
    {
        public bool IsAlive { get; }
        public Transform Transform { get; }
        public void Hit(HitArgs args);
    }
}