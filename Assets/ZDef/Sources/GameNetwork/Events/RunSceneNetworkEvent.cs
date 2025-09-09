using System.IO;

namespace ZDef.GameNetwork.Events
{

    public readonly struct RunSceneNetworkEvent
    {
        public string SceneName { get; }
        
        public RunSceneNetworkEvent(string sceneName)
        {
            SceneName = sceneName;
        }

        public class Serializer : IGameNetworkEventSerializer<RunSceneNetworkEvent>
        {
            public void Serialize(RunSceneNetworkEvent value, BinaryWriter writer)
            {
                writer.Write(value.SceneName);
            }

            public RunSceneNetworkEvent Deserialize(BinaryReader reader)
            {
                var sceneName = reader.ReadString();
                return new RunSceneNetworkEvent(sceneName);
            }
        }
    }
}