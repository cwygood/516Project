using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Common.Consts;
using Infrastructure.Common.Tools;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.HomeCommands
{
    public class LoginCommandHandler : IRequestHandler<LoginRequestCommand, LoginResponseCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtToken _jwtToken;
        private readonly IRedisCache _cache;
        public LoginCommandHandler(IUserRepository userRepository, IJwtToken jwtToken, IRedisCache cache)
        {
            this._userRepository = userRepository;
            this._jwtToken = jwtToken;
            this._cache = cache;
        }
        public Task<LoginResponseCommand> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            var validateCode = this._cache.GetString($"ValidateKey_{request.ValidateKey}", Consts.MainRedisKey);
            if (!string.Equals(request.ValidateCode, validateCode, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(ApiResult.Failure<LoginResponseCommand>("验证码错误", request.RequestId));
            }
            var userInfo = this._userRepository.GetUserInfo(request.UserName);
            if (userInfo == null)
            {
                return Task.FromResult(ApiResult.Failure<LoginResponseCommand>("用户不存在", request.RequestId));
            }
            //md5加密
            //if (!EncryptHelper.ValidateMd5(userInfo.Password, request.Password))
            //{
            //    return Task.FromResult(ApiResult.Failure<LoginResponseCommand>("密码错误", request.RequestId));
            //}
            if (!string.Equals(userInfo.Password, request.Password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(ApiResult.Failure<LoginResponseCommand>("密码错误", request.RequestId));
            }
            //todo; 获取用户所属角色
            List<Claim> claims = new List<Claim>();
            //if (!string.IsNullOrWhiteSpace(request.Role))
            //{
            //    claims.Add(new Claim("Roles", request.Role));
            //}
            var token = this._jwtToken.GetToken(claims.ToArray());
            if (string.IsNullOrWhiteSpace(token))
            {
                return Task.FromResult(ApiResult.Failure<LoginResponseCommand>("生成token失败", request.RequestId));
            }
            var userToken = Guid.NewGuid().ToString("N");
            this._cache.SetString($"UserInfo_{userToken}", JsonConvert.SerializeObject(userInfo), TimeSpan.FromMinutes(30), Consts.MainRedisKey);
            return Task.FromResult(ApiResult.OK<LoginResponseCommand>("登录成功！", request.RequestId, new
            {
                userName = userInfo.Name,
                userCode = userInfo.Code,
                userToken = userToken,
                token = token
            }));
        }
    }
}
