namespace ZDef.GameNetwork
{
    public class CreateRoomRequest
    {
        public string RoomName { get; }
        public CreateRoomRequest(string roomName)
        {
            RoomName = roomName;
        }
    }
}