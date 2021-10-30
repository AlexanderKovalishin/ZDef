using System;
using UnityEngine;

namespace ZDef.Game.Weapon
{
    [Serializable]
    public class FirearmsGun
    {
        [SerializeField] private ProjectileControllerFactory _factory;
        [SerializeField] private Transform _anchor;

        public ProjectileControllerFactory Factory => _factory;
        public Transform Anchor => _anchor;
    }
}