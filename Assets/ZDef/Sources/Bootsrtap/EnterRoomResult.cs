namespace ZDef.Bootsrtap
{
    public readonly struct EnterRoomResult
    {
        public string RoomId { get; }
        public DialogResult DialogResult { get; }

        public EnterRoomResult(string roomId, DialogResult dialogResult)
        {
            RoomId = roomId;
            DialogResult = dialogResult;
        }
    }
}