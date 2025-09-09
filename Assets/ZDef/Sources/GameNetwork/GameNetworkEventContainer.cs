using System;
using System.IO;
using Photon.Client;

namespace ZDef.GameNetwork
{
    public class GameNetworkEventContainer<TEvent> : GameNetworkEventContainer
    {
        public byte EventCode { get; }

        private readonly IGameNetworkEventSerializer<TEvent> _serializer;
        private Action<TEvent> _listeners;

        public GameNetworkEventContainer(IGameNetworkEventSerializer<TEvent> serializer, byte eventCode)
        {
            _serializer = serializer;
            EventCode = eventCode;
        }

        public byte[] Serialize(TEvent eventData)
        {
            using var stream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(stream);
            _serializer.Serialize(eventData, binaryWriter);
            return stream.ToArray();
        }

        public override void DeserializeAndInvoke(EventData eventData)
        {
            var bytes = (ByteArraySlice) eventData.CustomData;
            using var stream = new MemoryStream(bytes.Buffer, bytes.Offset, bytes.Count);
            using var binaryReader = new BinaryReader(stream);
            var args = _serializer.Deserialize(binaryReader);
            _listeners?.Invoke(args);
        }

        public void AddListener(Action<TEvent> listener)
        {
            _listeners += listener;
        }

        public void RemoveListener(Action<TEvent> listener)
        {
            _listeners -= listener;
        }
    }

    public abstract class GameNetworkEventContainer
    {
        public abstract void DeserializeAndInvoke(EventData eventData);
    }
}