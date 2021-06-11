using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Common.Mq;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.HomeCommands
{
    public class GetAllUserCommandHandler : IRequestHandler<GetAllUserRequestCommand, GetAllUserResponseCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRedisCache _cache;
        private readonly RabbitMqClient _mqClient;
        public GetAllUserCommandHandler(IUserRepository userRepository, IRedisCache cache, RabbitMqClient mqClient)
        {
            this._userRepository = userRepository;
            this._cache = cache;
            this._mqClient = mqClient;
        }
        public Task<GetAllUserResponseCommand> Handle(GetAllUserRequestCommand request, CancellationToken cancellationToken)
        {
            var userInfos = this._userRepository.GetAllUser();
            if (!userInfos.Any())
            {
                return Task.FromResult(new GetAllUserResponseCommand() { Code = "1", IsSuccess = false, Messages = new List<string>() { "查询数据失败！" } });
            }
            this._mqClient.PushMessage("user.test", userInfos, "516project");
            return Task.FromResult(new GetAllUserResponseCommand()
            {
                RequestId = request.RequestId,
                Code = "200",
                IsSuccess = true,
                Messages = new List<string>() { "成功！" },
                Data = userInfos
            });
        }
    }
}
