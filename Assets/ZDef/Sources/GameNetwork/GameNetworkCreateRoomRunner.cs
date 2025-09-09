using Cysharp.Threading.Tasks;
using Photon.Realtime;
using ZDef.Bootsrtap;

namespace ZDef.GameNetwork
{
    public class GameNetworkCreateRoomRunner
    {
        private readonly RealtimeClient _realtimeClient;
        private readonly MatchmakingCallbacks _matchmakingCallbacks;
        private readonly UniTaskCompletionSource<GameNetworkResponse<RoomResponseData>> _completion = new();
        
        public GameNetworkCreateRoomRunner(RealtimeClient realtimeClient, MatchmakingCallbacks matchmakingCallbacks)
        {
            _realtimeClient = realtimeClient;
            _matchmakingCallbacks = matchmakingCallbacks;
        }
        
        public async UniTask<GameNetworkResponse<RoomResponseData>> Run(CreateRoomRequest request)
        {
            if (_realtimeClient.CurrentRoom != null) return new GameNetworkResponse<RoomResponseData>(new RoomResponseData(_realtimeClient.CurrentRoom, GameMode.Host));
            _matchmakingCallbacks.CreatedRoom += MatchmakingCallbacksOnCreatedRoom;
            _matchmakingCallbacks.CreateRoomFailed += MatchmakingCallbacksOnCreateRoomFailed;
            var couldCreateRoom = _realtimeClient.OpCreateRoom(new EnterRoomArgs
            {
                RoomName = request.RoomName,
                RoomOptions = new RoomOptions
                {
                    MaxPlayers = request.PlayersCount
                }
            });
            if (!couldCreateRoom)
            {
                _matchmakingCallbacks.CreatedRoom -= MatchmakingCallbacksOnCreatedRoom;
                _matchmakingCallbacks.CreateRoomFailed -= MatchmakingCallbacksOnCreateRoomFailed;
                return new GameNetworkResponse<RoomResponseData>("Failed to create room");
            }
            var result = await _completion.Task;
            _matchmakingCallbacks.CreatedRoom -= MatchmakingCallbacksOnCreatedRoom;
            _matchmakingCallbacks.CreateRoomFailed -= MatchmakingCallbacksOnCreateRoomFailed;
            return result;
        }

        private void MatchmakingCallbacksOnCreateRoomFailed(short code, string error)
        {
            _completion.TrySetResult(new GameNetworkResponse<RoomResponseData>(error));
        }

        private void MatchmakingCallbacksOnCreatedRoom()
        {
            _completion.TrySetResult(
                new GameNetworkResponse<RoomResponseData>(
                    new RoomResponseData(_realtimeClient.CurrentRoom, GameMode.Host)));
        }
    }
}