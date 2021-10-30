using System.Numerics;
using UnityEngine;

namespace ZDef.Game.BusEvents
{
    public readonly struct PlayerHitEvent
    {
        public int Damage { get; }
        public PlayerHitEvent(int damage)
        {
            Damage = damage;
        }
    }
}