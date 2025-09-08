using Photon.Realtime;

namespace ZDef.GameNetwork
{
    public class CreateRoomResponseData
    {
        public Room Room { get; }

        public CreateRoomResponseData(Room room)
        {
            Room = room;
        }
    }
}