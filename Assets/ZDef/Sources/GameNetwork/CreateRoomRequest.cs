namespace ZDef.GameNetwork
{
    public class CreateRoomRequest
    {
        public string RoomName { get; }
        public int PlayersCount { get; }

        public CreateRoomRequest(string roomName, int playersCount)
        {
            RoomName = roomName;
            PlayersCount = playersCount;
        }
    }

}