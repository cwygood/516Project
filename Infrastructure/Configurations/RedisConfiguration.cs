using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class RedisConfiguration
    {
        public RedisHostConfig HostConfig { get; set; }
        public IEnumerable<RedisHostConfig> SentinelPoints { get; set; }
    }

    public class RedisHostConfig
    {
        public string MasterName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public int DBIndex { get; set; }
    }
}
