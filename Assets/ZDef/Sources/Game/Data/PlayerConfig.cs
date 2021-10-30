using UnityEngine;

namespace ZDef.Game.Data
{
    [CreateAssetMenu(menuName = "ZDef/PlayerConfig", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig: ScriptableObject
    {
        [SerializeField] private float _dieSloMo = 0.25f;
        [SerializeField] private float _dieDelay = 5;
        [SerializeField] private float _velocity;
        [SerializeField] private int _health;

        public float DieSloMo => _dieSloMo;
        public float DieDelay => _dieDelay;
        public int Health => _health;
        public float Velocity => _velocity;
    }
}