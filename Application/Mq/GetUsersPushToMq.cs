using Infrastructure.Common.Mq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mq
{
    public class GetUsersPushToMq
    {
        private readonly RabbitMqClient _mqClient;
        public GetUsersPushToMq(RabbitMqClient mqClient)
        {
            this._mqClient = mqClient;
        }

        public void PushMessage<T>(T message)
        {
            this._mqClient.PushMessage("user.test", message, "516project");
        }
    }
}
