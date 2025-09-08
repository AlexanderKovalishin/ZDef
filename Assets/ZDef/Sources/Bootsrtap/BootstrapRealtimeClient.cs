using System.Collections.Generic;
using Photon.Client;
using Photon.Realtime;
using UnityEngine;

namespace ZDef.Bootsrtap
{
    public class BootstrapRealtimeClient: MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks
    {
        private RealtimeClient _realtimeClient;
        private void Awake()
        {
            _realtimeClient = new RealtimeClient();
            _realtimeClient.AddCallbackTarget(this);
        }

        public void CallConnect(AppSettings appSettings)
        {
            var couldConnect = _realtimeClient.ConnectUsingSettings(appSettings);
            if (!couldConnect)
            {
                _realtimeClient.DebugReturn(LogLevel.Error, "Failed to connect.");
            }
        }

        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            CreateRoom();
        }
        
        private void CreateRoom()
        {
            // room creation arguments
            EnterRoomArgs enterRoomArgs = new EnterRoomArgs
            {
                RoomName = "112235",
                RoomOptions = new RoomOptions
                {
                }
            };

            _realtimeClient.OpCreateRoom(enterRoomArgs);
        }

        public void OnDisconnected(DisconnectCause cause)
        {
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            throw new System.NotImplementedException();
        }

        public void OnCreatedRoom()
        {
            // room created
            Debug.Log(_realtimeClient.CurrentRoom.Name);
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            throw new System.NotImplementedException();
        }

        public void OnJoinedRoom()
        {
            throw new System.NotImplementedException();
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            throw new System.NotImplementedException();
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            throw new System.NotImplementedException();
        }

        public void OnLeftRoom()
        {
            throw new System.NotImplementedException();
        }
    }
}