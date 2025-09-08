using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Photon.Realtime;
using UnityEngine;

namespace ZDef.GameNetwork
{
    public class GameNetworkApiClient 
    {
        private readonly RealtimeClient _realtimeClient = new ();
        private readonly ConnectionCallbacks _connectionCallbacks = new();

        public GameNetworkApiClient()
        {
            _realtimeClient.AddCallbackTarget(_connectionCallbacks);
            _realtimeClient.StateChanged += RealtimeClientOnStateChanged;
        }

        private void RealtimeClientOnStateChanged(ClientState arg1, ClientState arg2)
        {
            Debug.Log($"RealtimeClient State = {_realtimeClient.State}");
        }

        public async UniTask<GameNetworkResponse<ConnectResponseData>> Connect(ConnectRequest request)
        {
            return await new GameNetworkConnectionRunner(_realtimeClient, _connectionCallbacks).Run(request);
        }

        public async Task<GameNetworkResponse<CreateRoomResponseData>> CreateRoom(CreateRoomRequest request)
        {
            return null;
        }
    }

}