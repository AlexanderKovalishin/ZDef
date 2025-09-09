using Photon.Realtime;
using ZDef.GameNetwork;
using ZDef.GameNetwork.Events;
using Zenject;

namespace ZDef.Bootsrtap
{
    public class PhotonInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            var realtimeClient = new RealtimeClient();
            var gameNetworkEventBus = new GameNetworkEventBus(realtimeClient);  
            BindNetworkEvents(gameNetworkEventBus);

            Container.BindInstance(gameNetworkEventBus);

            Container.Bind(typeof(GameNetworkApiClient), typeof(ITickable))
                .To<GameNetworkApiClient>()
                .AsSingle()
                .WithArguments(realtimeClient);
        }

        private void BindNetworkEvents(GameNetworkEventBus gameNetworkEventBus)
        {
            gameNetworkEventBus.Register(new RunSceneNetworkEvent.Serializer());
            gameNetworkEventBus.Register(new MovePlayerNetworkEvent.Serializer());
            
        }
    }
}