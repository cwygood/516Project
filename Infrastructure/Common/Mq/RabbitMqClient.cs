using Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Mq
{
    public class RabbitMqClient
    {
        private readonly ILogger<RabbitMqClient> _logger;
        private readonly IModel _channel;
        public RabbitMqClient(IOptionsMonitor<RabbitMqConfiguration> options, ILogger<RabbitMqClient> logger)
        {
            this._logger = logger;
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = options.CurrentValue.Host,
                    UserName = options.CurrentValue.User,
                    Password = options.CurrentValue.Password,
                    Port = options.CurrentValue.Port
                };
                var connection = factory.CreateConnection();
                this._channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                this._logger.LogError(-1, ex, "RabbitMqClient Init Fail");
            }
        }

        public virtual void PushMessage(string routeKey, object message, string exchangeName)
        {
            this._logger.LogInformation($"PushMessage routeKey:{routeKey}");
            this._channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
            //this._channel.QueueDeclare(
            //    queue: routeKey,//队列名称
            //    durable: false,//是否持久化，保存到磁盘
            //    exclusive: false,//设置是否排他。true排他的。如果一个队列声明为排他队列，该队列仅对首次声明它的连接可见，并在连接断开时自动删除。
            //    /*
            //     * 排它是基于连接可见的，同一个连接不同信道是可以访问同一连接创建的排它队列，“首次”是指如果一个连接已经声明了一个排他队列，
            //     * 其他连接是不允许建立同名的排他队列，即使这个队列是持久化的，一旦连接关闭或者客户端退出，该排它队列会被自动删除，这种队列适用于一个客户端同时发送与接口消息的场景。
            //     */
            //    autoDelete: false,//设置是否自动删除。true是自动删除。自动删除的前提是：致少有一个消费者连接到这个队列，之后所有与这个队列连接的消费者都断开 时，才会自动删除,没有消费者客户端与这个队列连接时，不会自动删除这个队列
            //    arguments: null//设置队列的其他参数
            //    );
            var msgJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(msgJson);
            this._channel.BasicPublish(exchange: exchangeName, routingKey: routeKey, basicProperties: null, body: body);
        }

    }
}
