using Photon.Realtime;

namespace ZDef.GameNetwork
{
    public class ConnectRequest
    {
        public AppSettings AppSettings { get; }

        public ConnectRequest(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }
    }
}