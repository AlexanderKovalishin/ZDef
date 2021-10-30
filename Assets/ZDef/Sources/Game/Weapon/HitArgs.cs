using UnityEngine;

namespace ZDef.Game.Weapon
{
    public readonly struct HitArgs
    {
        public HitArgs(int damage, Vector3 source)
        {
            Damage = damage;
            Source = source;
        }

        public int Damage { get; }
        public Vector3 Source { get; }
    }
}