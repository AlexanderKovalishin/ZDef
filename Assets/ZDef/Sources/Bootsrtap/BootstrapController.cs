using Cysharp.Threading.Tasks;
using Photon.Client;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZDef.GameNetwork;
using ZDef.GameNetwork.Events;
using Zenject;

namespace ZDef.Bootsrtap
{
    public class BootstrapUIController : MonoBehaviour
    {
        [SerializeField] private BootstrapUILoading _loading;
        [SerializeField] private BootstrapUIMessage _message;
        [SerializeField] private BootstrapUIMenu _menu;
        [SerializeField] private BootstrapUIEnterRoom _enterRoom;
        [SerializeField] private BootstrapUIHostRoomMenu _hostRoomMenu;
        [SerializeField] private BootstrapUIClientRoomMenu _clientRoomMenu;
        
        [Inject] private GameNetworkApiClient _network;
        [Inject] private GameNetworkEventBus _networkEventBus;

        private void Start()
        {
            Run();
        }

        private async void Run()
        {
            await Connect();
            var roomData= await RunGame();
            
            if (roomData.Mode != GameMode.Host) return;
            
            _networkEventBus.Send(new RunSceneNetworkEvent("ZDefCoreGameScene"));
            SceneManager.LoadScene("ZDefCoreGameScene");
        }

        private async UniTask Connect()
        {
            while (true)
            {
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

                if (response.Status == ResponseStatus.Failed)
                {
                    await _message.ShowPopup(response.ErrorMessage);
                }
                else
                {
                    break;
                }

            }
        }

        private async UniTask<RoomResponseData> RunGame()
        {
            while (true)
            {
                var roomResponse = await EnterRoom();
                if (roomResponse.Mode == GameMode.Host)
                {
                    var result = await _hostRoomMenu.ShowPopup(roomResponse.Room);
                    if (result == DialogResult.Cancel)
                    {
                        await ExitRoom();
                    }
                    if (result == DialogResult.Ok)
                    {
                        return roomResponse;
                    }
                }
                else
                {
                    // todo start listen start game remote event
                    _networkEventBus.Subscribe<RunSceneNetworkEvent>(ClientRunScene);
                    var result = await _clientRoomMenu.ShowPopup(roomResponse.Room);
                    _networkEventBus.UnSubscribe<RunSceneNetworkEvent>(ClientRunScene);
                    // todo stop listen start game remote event
                    if (result == DialogResult.Cancel)
                    {
                        await ExitRoom();
                    }
                }
            }
        }

        private void ClientRunScene(RunSceneNetworkEvent eventData)
        {
            _networkEventBus.UnSubscribe<RunSceneNetworkEvent>(ClientRunScene);
            // todo:load client scene 
            SceneManager.LoadScene("ZDefClientInputScene");
        }

        private async UniTask<RoomResponseData> EnterRoom()
        {
            while (true)
            {
                var menuResult = await _menu.ShowPopup();
                if (menuResult == StartMenuAction.CreateRoom)
                {
                    var createRoomResponse = await CreatRoom();
                    if (createRoomResponse.Status == ResponseStatus.Success)
                    {
                        return createRoomResponse.ResponseData;
                    }
                }
                else if (menuResult == StartMenuAction.EnterRoom)
                {
                    var enterRoomResult = await _enterRoom.ShowPopup();
                    if (enterRoomResult.DialogResult == DialogResult.Ok)
                    {
                        var joinRoomResponse = await JoinRoom(enterRoomResult.RoomId);
                        if (joinRoomResponse.Status == ResponseStatus.Success)
                        {
                            return joinRoomResponse.ResponseData;
                        }
                    }
                }
            }
        }


        private async UniTask<GameNetworkResponse<ExitRoomResponseData>> ExitRoom()
        {
            await _loading.Show();
            var response = await _network.ExitRoom(new ExitRoomRequest());
            await _loading.Hide();
            
            if (response.Status == ResponseStatus.Failed)
            {
                await _message.ShowPopup(response.ErrorMessage);
            }
            
            return response;
        }

        private async UniTask<GameNetworkResponse<RoomResponseData>> CreatRoom()
        {
            await _loading.Show();
            var response = await _network.CreateRoom(new CreateRoomRequest(PasswordGenerator.Generate().ToLower(), 3));
            await _loading.Hide();

            if (response.Status == ResponseStatus.Failed)
            {
                await _message.ShowPopup(response.ErrorMessage);
            }

            return response;
        }
        
        private async UniTask<GameNetworkResponse<RoomResponseData>> JoinRoom(string roomId)
        {
            await _loading.Show();
            var response = await _network.JoinRoom(new JoinRoomRequest(roomId));
            await _loading.Hide();
            
            if (response.Status == ResponseStatus.Failed)
            {
                await _message.ShowPopup(response.ErrorMessage);
            }

            return response;
        }
    }
}
