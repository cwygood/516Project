using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class RedisConfiguration
    {
        /// <summary>
        /// 单机模式
        /// </summary>
        public RedisHostConfig HostConfig { get; set; }
        /// <summary>
        /// 哨兵模式（Sentinel）
        /// </summary>
        public IEnumerable<RedisHostConfig> SentinelPoints { get; set; }
        /// <summary>
        /// 分片模式（Cluster）
        /// </summary>
        public IEnumerable<RedisHostConfig> ClusterPoints { get; set; }
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
