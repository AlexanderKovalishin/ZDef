using Photon.Client;
using Photon.Realtime;
using UnityEngine;
using ZDef.GameNetwork;
using Zenject;

namespace ZDef.Bootsrtap
{
    public class BootstrapUIController : MonoBehaviour
    {
        [SerializeField] private BootstrapUILoading _loading;
        [SerializeField] private BootstrapUIMenu _menu;
        
        [Inject] private GameNetworkApiClient _network;

        private void Awake()
        {
            _menu.CreatRoomClick += MenuOnCreatRoomClick;
        }

        private void Start()
        {
            Run();
        }

        private async void Run()
        {
            // todo: show loading
            await _loading.Show();
            Debug.Log("Start connecting");
            var response = await _network.Connect(new ConnectRequest(new AppSettings
            {
                AppIdRealtime = "9e221d77-f9d1-44a2-b314-02d395a8479b",
                AppVersion = "1",
                UseNameServer = true,
                Protocol = ConnectionProtocol.Udp,
                EnableLobbyStatistics = false,
                NetworkLogging = LogLevel.Debug
            }));

            Debug.Log($"Connection complete with status {response.Status}");
            await _loading.Hide();

            await _menu.Show();
        }
        
        private async void MenuOnCreatRoomClick()
        {
            await _menu.Hide();
            await _loading.Show();
            var createRoomResponse = await _network.CreateRoom(new CreateRoomRequest(PasswordGenerator.Generate()));
            await _loading.Hide();
            if (createRoomResponse.Status != ResponseStatus.Success)
            {
                // todo: show error message
                await _menu.Show();
                return;
            }
            Debug.Log(createRoomResponse.ResponseData.Room.Name);
        }
    }
}
