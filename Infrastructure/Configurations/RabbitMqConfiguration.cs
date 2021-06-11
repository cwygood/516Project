using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class RabbitMqConfiguration
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
