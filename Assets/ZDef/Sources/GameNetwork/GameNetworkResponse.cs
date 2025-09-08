namespace ZDef.GameNetwork
{
    public class GameNetworkResponse<TResponseData>
    {
        public ResponseStatus Status { get; }
        public TResponseData ResponseData { get; }
        public string ErrorMessage { get; }

        public GameNetworkResponse(TResponseData responseData)
        {
            Status = ResponseStatus.Success;
            ResponseData = responseData;
            ErrorMessage = string.Empty;
        }

        public GameNetworkResponse(string errorMessage)
        {
            Status = ResponseStatus.Failed;
            ResponseData = default;
            ErrorMessage = errorMessage;
        }
    }
}