using ZDef.GameNetwork;
using Zenject;

namespace ZDef.Bootsrtap
{
    public class PhotonInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(GameNetworkApiClient), typeof(ITickable))
                .To<GameNetworkApiClient>()
                .AsSingle();
        }
    }
}