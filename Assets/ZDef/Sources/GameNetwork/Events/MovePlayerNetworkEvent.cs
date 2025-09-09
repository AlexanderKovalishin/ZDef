using System.IO;
using UnityEngine;

namespace ZDef.GameNetwork.Events
{
    public readonly struct MovePlayerNetworkEvent
    {
        public string PlayerId { get; }
        public Vector2 Direction { get; }

        public MovePlayerNetworkEvent(string playerId, Vector2 direction)
        {
            PlayerId = playerId;
            Direction = direction;
        }
        
        public class Serializer : IGameNetworkEventSerializer<MovePlayerNetworkEvent>
        {
            public void Serialize(MovePlayerNetworkEvent value, BinaryWriter writer)
            {
                writer.Write(value.PlayerId);
                writer.Write(value.Direction.x);
                writer.Write(value.Direction.y);
            }

            public MovePlayerNetworkEvent Deserialize(BinaryReader reader)
            {
                var playerId = reader.ReadString();
                var x = reader.ReadSingle();
                var y = reader.ReadSingle();
                return new MovePlayerNetworkEvent(playerId, new Vector2(x, y));
            }
        }
    }
}