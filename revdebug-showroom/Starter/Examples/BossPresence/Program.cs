using System.Xml.Serialization;

namespace Starter.Examples.BossPresence
{
    public class BossPresence
    {
        public string GetBossImage()
        {
            var reader = new XmlSerializer(typeof(Config));
            var file = new System.IO.StreamReader(Constants.ConfigFileName);
            var boss = (Config)reader.Deserialize(file);

            var networkUtils = new NetworkUtils();
            var checkPoint = networkUtils.CheckTcpConnection(boss.IpAddress);

            return checkPoint ? Constants.BossFront : Constants.BossBack;
        }
    }
}
