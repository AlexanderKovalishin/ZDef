using UnityEngine;

namespace ZDef.Game.Data
{
    [CreateAssetMenu(menuName = "ZDef/FirearmsConfig", fileName = "FirearmsConfig", order = 0)]
    public class FirearmsConfig: ScriptableObject
    {
        [SerializeField] private float _projectileVelocity;
        [SerializeField] private float _shotsPerSecond;

        public float ProjectileVelocity => _projectileVelocity;
        public float ShotsPerSecond => _shotsPerSecond;
        public float ShotsTimeout => 1f / _shotsPerSecond;
    }
}