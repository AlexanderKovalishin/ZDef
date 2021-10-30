using UnityEngine;

namespace ZDef.Game.Fx
{
    public readonly struct HitFxInitArgs
    {
        public Vector3 Position { get; }
        public Vector3 Source { get; }

        public HitFxInitArgs(Vector3 position, Vector3 source)
        {
            Position = position;
            Source = source;
        }
    }
}