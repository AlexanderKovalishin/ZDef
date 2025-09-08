using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace ZDef.GameNetwork
{
    public class ConnectionCallbacks : IConnectionCallbacks
    {
        public event Action ConnectedToMaster;
        public event Action<DisconnectCause> Disconnected;
        public event Action<RegionHandler> RegionListReceived;
        public event Action<Dictionary<string, object>> CustomAuthenticationResponse;
        public event Action<string> CustomAuthenticationFailed;
        
        public void OnConnected() 
        { 
            // obsolete
            Debug.Log("ConnectionCallbacks.OnConnected");
        }

        public void OnConnectedToMaster()
        {
            Debug.Log("ConnectionCallbacks.OnConnectedToMaster");
            ConnectedToMaster?.Invoke();
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($"ConnectionCallbacks.OnDisconnected({cause})");
            Disconnected?.Invoke(cause);
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("ConnectionCallbacks.OnRegionListReceived");
            RegionListReceived?.Invoke(regionHandler);
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            Debug.Log("ConnectionCallbacks.OnCustomAuthenticationResponse");
            CustomAuthenticationResponse?.Invoke(data);
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log("ConnectionCallbacks.OnCustomAuthenticationFailed");
            CustomAuthenticationFailed?.Invoke(debugMessage);
        }
    }
}