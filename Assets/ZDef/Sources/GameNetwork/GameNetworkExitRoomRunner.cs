using Cysharp.Threading.Tasks;
using Photon.Realtime;

namespace ZDef.GameNetwork
{
    public class GameNetworkExitRoomRunner
    {
        private readonly RealtimeClient _realtimeClient;
        private readonly MatchmakingCallbacks _matchmakingCallbacks;
        private readonly UniTaskCompletionSource<GameNetworkResponse<ExitRoomResponseData>> _completion = new();
        
        public GameNetworkExitRoomRunner(RealtimeClient realtimeClient, MatchmakingCallbacks matchmakingCallbacks)
        {
            _realtimeClient = realtimeClient;
            _matchmakingCallbacks = matchmakingCallbacks;
        }
        
        public async UniTask<GameNetworkResponse<ExitRoomResponseData>> Run(ExitRoomRequest request)
        {
            if (_realtimeClient.CurrentRoom == null)
                return new GameNetworkResponse<ExitRoomResponseData>("Failed to exit room");
            _matchmakingCallbacks.LeftRoom += MatchmakingCallbacksOnLeftRoom;
            var couldExitRoom = _realtimeClient.OpLeaveRoom(true);
            if (!couldExitRoom)
            {
                _matchmakingCallbacks.LeftRoom -= MatchmakingCallbacksOnLeftRoom;
                return new GameNetworkResponse<ExitRoomResponseData>("Failed to exit room");
            }
            var result = await _completion.Task;
            _matchmakingCallbacks.LeftRoom -= MatchmakingCallbacksOnLeftRoom;
            return result;
        }

        private void MatchmakingCallbacksOnLeftRoom()
        {
            _completion.TrySetResult(
                new GameNetworkResponse<ExitRoomResponseData>(
                    new ExitRoomResponseData()));
        }
    }
}