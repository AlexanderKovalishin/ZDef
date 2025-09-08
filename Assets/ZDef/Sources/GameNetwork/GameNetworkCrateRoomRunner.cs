using Cysharp.Threading.Tasks;
using Photon.Realtime;

namespace ZDef.GameNetwork
{
    public class GameNetworkCrateRoomRunner
    {
        private readonly RealtimeClient _realtimeClient;
        private readonly MatchmakingCallbacks _matchmakingCallbacks;
        private readonly UniTaskCompletionSource<GameNetworkResponse<CreateRoomResponseData>> _completion = new();
        
        public GameNetworkCrateRoomRunner(RealtimeClient realtimeClient, MatchmakingCallbacks matchmakingCallbacks)
        {
            _realtimeClient = realtimeClient;
            _matchmakingCallbacks = matchmakingCallbacks;
        }
        
        public async UniTask<GameNetworkResponse<CreateRoomResponseData>> Run(CreateRoomRequest request)
        {
            if (_realtimeClient.CurrentRoom != null) return new GameNetworkResponse<CreateRoomResponseData>(new CreateRoomResponseData(_realtimeClient.CurrentRoom));
            _matchmakingCallbacks.CreatedRoom += MatchmakingCallbacksOnCreatedRoom;
            _matchmakingCallbacks.CreateRoomFailed += MatchmakingCallbacksOnCreateRoomFailed;
            var couldCreateRoom = _realtimeClient.OpCreateRoom(new EnterRoomArgs {RoomName = request.RoomName});
            if (!couldCreateRoom)
            {
                _matchmakingCallbacks.CreatedRoom -= MatchmakingCallbacksOnCreatedRoom;
                _matchmakingCallbacks.CreateRoomFailed -= MatchmakingCallbacksOnCreateRoomFailed;
                return new GameNetworkResponse<CreateRoomResponseData>("Failed to create room");
            }
            var result = await _completion.Task;
            _matchmakingCallbacks.CreatedRoom -= MatchmakingCallbacksOnCreatedRoom;
            _matchmakingCallbacks.CreateRoomFailed -= MatchmakingCallbacksOnCreateRoomFailed;
            return result;
        }

        private void MatchmakingCallbacksOnCreateRoomFailed(short code, string error)
        {
            _completion.TrySetResult(new GameNetworkResponse<CreateRoomResponseData>(error));
        }

        private void MatchmakingCallbacksOnCreatedRoom()
        {
            _completion.TrySetResult(
                new GameNetworkResponse<CreateRoomResponseData>(
                    new CreateRoomResponseData(_realtimeClient.CurrentRoom)));
        }
    }
}