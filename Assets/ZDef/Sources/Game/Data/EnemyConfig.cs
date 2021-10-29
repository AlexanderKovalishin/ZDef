using UnityEngine;

namespace ZDef.Game.Data
{
    [CreateAssetMenu(menuName = "ZDef/EnemyConfig", fileName = "EnemyConfig", order = 0)]
    public class EnemyConfig: ScriptableObject
    {
        [SerializeField] private int _probabilityWeight = 10;
        [SerializeField] private int _health = 100;
        [SerializeField] private float _minVelocity = 1;
        [SerializeField] private float _maxVelocity = 1;
        [SerializeField] private string _skin = string.Empty;

        public int Health => _health;
        public int ProbabilityWeight => _probabilityWeight;
        public string Skin => _skin;
        public float GetRandomVelocity() => Random.Range(_minVelocity, _maxVelocity);
    }
}