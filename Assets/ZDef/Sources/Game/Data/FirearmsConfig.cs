using UnityEngine;

namespace ZDef.Game.Data
{
    [CreateAssetMenu(menuName = "ZDef/FirearmsConfig", fileName = "FirearmsConfig", order = 0)]
    public class FirearmsConfig: ScriptableObject
    {
        [SerializeField] private float _projectileVelocity;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _shotsPerSecond;

        public float ProjectileVelocity => _projectileVelocity;
        public float Damage => _damage;
        public float ShotsTimeout => 1f / _shotsPerSecond;
    }
}