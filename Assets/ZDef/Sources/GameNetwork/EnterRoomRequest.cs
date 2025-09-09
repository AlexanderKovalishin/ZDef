namespace ZDef.GameNetwork
{

    public class JoinRoomRequest
    {
        public string RoomName { get; }
        
        public JoinRoomRequest(string roomName)
        {
            RoomName = roomName;
        }
    }
}