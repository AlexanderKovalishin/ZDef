using Photon.Realtime;
using ZDef.Bootsrtap;

namespace ZDef.GameNetwork
{

    public class RoomResponseData
    {
        public GameMode Mode { get; }
        public Room Room { get; }

        public RoomResponseData(Room room, GameMode mode)
        {
            Room = room;
            Mode = mode;
        }
    }

}