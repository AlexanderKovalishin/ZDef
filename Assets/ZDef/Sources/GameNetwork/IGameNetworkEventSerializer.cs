using System.IO;

namespace ZDef.GameNetwork
{
    public interface IGameNetworkEventSerializer<T>
    {
        public void Serialize(T value, BinaryWriter writer);
        public T Deserialize(BinaryReader reader);
    }
}