using Cysharp.Threading.Tasks;
using Photon.Realtime;

namespace ZDef.GameNetwork
{
    public class GameNetworkConnectionRunner
    {
        private readonly RealtimeClient _realtimeClient;
        private readonly ConnectionCallbacks _connectionCallbacks;
        private readonly UniTaskCompletionSource _completion = new();

        public GameNetworkConnectionRunner(RealtimeClient realtimeClient, ConnectionCallbacks connectionCallbacks)
        {
            _realtimeClient = realtimeClient;
            _connectionCallbacks = connectionCallbacks;
        }

        public async UniTask<GameNetworkResponse<ConnectResponseData>> Run(ConnectRequest request)
        {
            if (_realtimeClient.IsConnectedAndReady) return new GameNetworkResponse<ConnectResponseData>(new ConnectResponseData());
            _connectionCallbacks.ConnectedToMaster += ConnectionCallbacksOnConnectedToMaster;
            var couldConnect = _realtimeClient.ConnectUsingSettings(request.AppSettings);
            if (!couldConnect)
            {
                _connectionCallbacks.ConnectedToMaster -= ConnectionCallbacksOnConnectedToMaster;
                return new GameNetworkResponse<ConnectResponseData>("Failed to connect");
            }
            await _completion.Task;
            _connectionCallbacks.ConnectedToMaster -= ConnectionCallbacksOnConnectedToMaster;
            return new GameNetworkResponse<ConnectResponseData>(new ConnectResponseData());
        }

        private void ConnectionCallbacksOnConnectedToMaster()
        {
            _completion.TrySetResult();
        }
    }
}