using System.Net.NetworkInformation;

namespace Starter.Examples.BossPresence
{
    public class NetworkUtils
    {
        public bool CheckTcpConnection(string ipAddress)
        {
            try
            {
                var ping = new Ping();
                var pingReplay = ping.Send(ipAddress);
                if (pingReplay != null && pingReplay.Status == IPStatus.Success)
                    return true;
            }
            catch
            {
                return false;
            }

            return false;
        }

    }
}
