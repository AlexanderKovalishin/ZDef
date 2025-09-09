using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Photon.Realtime;
using UnityEngine;
using ZDef.Utils;
using Zenject;

namespace ZDef.GameNetwork
{
    public class GameNetworkApiClient: ITickable
    {
        private readonly RealtimeClient _realtimeClient;
        private readonly ConnectionCallbacks _connectionCallbacks = new();
        private readonly MatchmakingCallbacks _matchmakingCallbacks = new();
        
        private readonly TimeKeeper _dispatchInterval = new(10);
        private readonly TimeKeeper _sendInterval = new(50);
        public GameNetworkApiClient(RealtimeClient realtimeClient)
        {
            _realtimeClient = realtimeClient;
            _realtimeClient.AddCallbackTarget(_connectionCallbacks);
            _realtimeClient.AddCallbackTarget(_matchmakingCallbacks);
            _realtimeClient.StateChanged += RealtimeClientOnStateChanged;
            _realtimeClient.RealtimePeer.UseByteArraySlicePoolForEvents = true;
        }

        private void RealtimeClientOnStateChanged(ClientState arg1, ClientState arg2)
        {
            Debug.Log($"RealtimeClient State = {_realtimeClient.State}");
        }

        public async UniTask<GameNetworkResponse<ConnectResponseData>> Connect(ConnectRequest request)
        {
            return await new GameNetworkConnectionRunner(_realtimeClient, _connectionCallbacks).Run(request);
        }

        public async UniTask<GameNetworkResponse<RoomResponseData>> CreateRoom(CreateRoomRequest request)
        {
            return await new GameNetworkCreateRoomRunner(_realtimeClient, _matchmakingCallbacks).Run(request);
        }

        public async UniTask<GameNetworkResponse<RoomResponseData>> JoinRoom(JoinRoomRequest request)
        {
            return await new GameNetworkJoinRoomRunner(_realtimeClient, _matchmakingCallbacks).Run(request);
        }
        
        public async UniTask<GameNetworkResponse<ExitRoomResponseData>> ExitRoom(ExitRoomRequest request)
        {
            return await new GameNetworkExitRoomRunner(_realtimeClient, _matchmakingCallbacks).Run(request);
        }

        public void Tick()
        {
            if (_dispatchInterval.ShouldExecute)
            {
                while (_realtimeClient.DispatchIncomingCommands())
                {
                    // You could count dispatch calls to limit them to X, if they take too much time of a single frame
                }
                _dispatchInterval.Reset();  // we dispatched, so reset the timer
            }

            if (_sendInterval.ShouldExecute)
            {
                _realtimeClient.SendOutgoingCommands();
                _sendInterval.Reset();
            }
        }

        public int[] RemotePlayerActorNumbers()
        {
            if (_realtimeClient.CurrentRoom == null)
                return Array.Empty<int>();
            return _realtimeClient.CurrentRoom.Players
                .Where(x => !x.Value.IsLocal)
                .Select(x => x.Value.ActorNumber).ToArray();
        }

        public int CurrentPlayerActorNumber()
        {
            return _realtimeClient.CurrentRoom.Players.FirstOrDefault(x => x.Value.IsLocal).Value.ActorNumber;
        }
    }

}