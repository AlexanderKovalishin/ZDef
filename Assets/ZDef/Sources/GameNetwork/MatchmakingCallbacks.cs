using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace ZDef.GameNetwork
{
    public class MatchmakingCallbacks : IMatchmakingCallbacks
    {
        public event Action<List<FriendInfo>> FriendListUpdate;
        public event Action CreatedRoom;
        public event Action<short, string> CreateRoomFailed;
        public event Action JoinedRoom;
        public event Action<short, string> JoinRoomFailed;
        public event Action<short, string> JoinRandomFailed;
        public event Action LeftRoom;
        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log("MatchmakingCallbacks.OnFriendListUpdate");
            FriendListUpdate?.Invoke(friendList);
        }

        public void OnCreatedRoom()
        {
            Debug.Log("MatchmakingCallbacks.OnCreatedRoom");
            CreatedRoom?.Invoke();
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"MatchmakingCallbacks.OnCreateRoomFailed({returnCode}, {message})");
            CreateRoomFailed?.Invoke(returnCode, message);
        }

        public void OnJoinedRoom()
        {
            Debug.Log("MatchmakingCallbacks.OnJoinedRoom");
            JoinedRoom?.Invoke();
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"MatchmakingCallbacks.OnJoinRoomFailed({returnCode}, {message})");
            JoinRoomFailed?.Invoke(returnCode, message);
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log($"MatchmakingCallbacks.OnJoinRandomFailed({returnCode}, {message})");
            JoinRandomFailed?.Invoke(returnCode, message);
        }

        public void OnLeftRoom()
        {
            Debug.Log("MatchmakingCallbacks.OnLeftRoom");
            LeftRoom?.Invoke();
        }
    }
}