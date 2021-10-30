using UnityEngine;

namespace ZDef.Game.Fx.Decals
{
    public readonly struct DecalsInitArgs
    {
        public Vector3 Position { get; }

        public DecalsInitArgs(Vector3 position)
        {
            Position = position;
        }
    }
}