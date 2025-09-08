using Photon.Client;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using ZDef.GameNetwork;

namespace ZDef.Bootsrtap
{

    public class BootstrapController : MonoBehaviour
    {
        [SerializeField] private Button _creatRoomButton;
        
        private readonly GameNetworkApiClient _network = new();

        private void Awake()
        {
            _creatRoomButton.onClick.AddListener(CreatRoomButtonOnClick);
        }

        private async void CreatRoomButtonOnClick()
        {
            Debug.Log("Start connecting");
            var response = await _network.Connect(new ConnectRequest(new AppSettings
            {
                AppIdRealtime = "9e221d77-f9d1-44a2-b314-02d395a8479b",
                AppVersion = "1.0",
                UseNameServer = true,
                Protocol = ConnectionProtocol.Udp,
                EnableLobbyStatistics = false,
                NetworkLogging = LogLevel.Debug
            }));

            Debug.Log($"Connection complete with status {response.Status}");
        }
    }
}
