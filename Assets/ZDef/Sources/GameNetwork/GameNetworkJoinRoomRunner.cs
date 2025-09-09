using Cysharp.Threading.Tasks;
using Photon.Realtime;
using ZDef.Bootsrtap;

namespace ZDef.GameNetwork
{

    public class GameNetworkJoinRoomRunner
    {
        private readonly RealtimeClient _realtimeClient;
        private readonly MatchmakingCallbacks _matchmakingCallbacks;
        private readonly UniTaskCompletionSource<GameNetworkResponse<RoomResponseData>> _completion = new();
        
        public GameNetworkJoinRoomRunner(RealtimeClient realtimeClient, MatchmakingCallbacks matchmakingCallbacks)
        {
            _realtimeClient = realtimeClient;
            _matchmakingCallbacks = matchmakingCallbacks;
        }
        
        public async UniTask<GameNetworkResponse<RoomResponseData>> Run(JoinRoomRequest request)
        {
            if (_realtimeClient.CurrentRoom != null)
                return new GameNetworkResponse<RoomResponseData>(new RoomResponseData(_realtimeClient.CurrentRoom,
                    GameMode.Client));
            _matchmakingCallbacks.JoinedRoom += MatchmakingCallbacksOnJoinedRoom;
            _matchmakingCallbacks.JoinRoomFailed += MatchmakingCallbacksOnJoinedRoomFailed;
            var couldJoinRoom = _realtimeClient.OpJoinRoom(new EnterRoomArgs {RoomName = request.RoomName});
            if (!couldJoinRoom)
            {
                _matchmakingCallbacks.JoinedRoom -= MatchmakingCallbacksOnJoinedRoom;
                _matchmakingCallbacks.JoinRoomFailed -= MatchmakingCallbacksOnJoinedRoomFailed;
                return new GameNetworkResponse<RoomResponseData>("Failed to join room");
            }
            var result = await _completion.Task;
            _matchmakingCallbacks.JoinedRoom -= MatchmakingCallbacksOnJoinedRoom;
            _matchmakingCallbacks.JoinRoomFailed -= MatchmakingCallbacksOnJoinedRoomFailed;
            return result;
        }

        private void MatchmakingCallbacksOnJoinedRoomFailed(short code, string error)
        {
            _completion.TrySetResult(new GameNetworkResponse<RoomResponseData>(error));
        }

        private void MatchmakingCallbacksOnJoinedRoom()
        {
            _completion.TrySetResult(
                new GameNetworkResponse<RoomResponseData>(
                    new RoomResponseData(_realtimeClient.CurrentRoom, GameMode.Client)));
        }
    }
}