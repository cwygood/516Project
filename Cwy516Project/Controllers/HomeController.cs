﻿using Application.Commands;
using Application.Commands.HomeCommands;
using Domain.Interfaces;
using Infrastructure.Common.Consts;
using Infrastructure.Common.Tools;
using Infrastructure.Configurations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwy516Project.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ITest _test;
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IRedisCache _cache;
        private readonly IMemoryCache _memoryCache;
        private readonly IOptionsMonitor<ConsulConfiguration> _options;
        public HomeController(ITest test, ILogger<HomeController> logger, IMediator mediator, IRedisCache cache, IMemoryCache memoryCache, IOptionsMonitor<ConsulConfiguration> options)
        {
            this._test = test;
            this._logger = logger;
            this._mediator = mediator;
            this._cache = cache;
            this._memoryCache = memoryCache;
            this._options = options;
        }

        #region Test
        [HttpGet]
        public ActionResult<string> Index()
        {
            Console.WriteLine($"Service:{this._options.CurrentValue.ServiceName},Address:{this._options.CurrentValue.ServiceHost}:{_options.CurrentValue.ServicePort}");
            //System.Threading.Thread.Sleep(1000 * 60);

            this._test.Show();
            this._logger.LogInformation("11111");
            this._cache.SetString("TestKey", "Hello");
            //this._cache.GetDatabase().StringSet("TestKey", "Hello");
            this._memoryCache.Set<string>("TestKey", "11111");
            return "helloworld";
        }
        /// <summary>
        /// 服务调用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new
            {
                name = this._options.CurrentValue.ServiceName,
                port = this._options.CurrentValue.ServicePort
            });
        }
        /// <summary>
        /// consul验证请求转发
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseResponseCommand> ConsulRequest(ConsulRequestCommand command)
        {
            return await this._mediator.Send(command);
        }
        /// <summary>
        /// consul健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> HealthCheck()
        {
            return "health";
        }
        [HttpGet]
        public ActionResult<string> OcelotA()
        {
            return "ServiceA" + Guid.NewGuid().ToString("N");//后台ocelot启用了缓存，所以在缓存时间的范围内，返回的内容不变
        }
        [HttpGet]
        public ActionResult<string> OcelotB()
        {
            return "ServiceB" + Guid.NewGuid().ToString("N");
        }
        [HttpPost]
        public JsonResult Post()
        {
            var ss = this._cache.GetString("TestKey");
            var val = this._memoryCache.Get<string>("TestKey");
            return new JsonResult(new
            {
                MyActionResult = val
            });
        }
        #endregion

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public async Task<GetAllUserResponseCommand> GetAllUser(GetAllUserRequestCommand command)
        {
            return await this._mediator.Send(command);
        }
        /// <summary>
        /// 查询单个用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<GetUserResponseCommand> GetUserById(GetUserRequestCommand command)
        {
            return await this._mediator.Send(command);
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AddUserResponseCommand> AddUser(AddUserRequestCommand command)
        {
            var ss = ModelState.IsValid;
            return await this._mediator.Send(command);
        }
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetTokenResponseCommand> GetToken()
        {
            //传入用户名和密码获取token=>用户名得到角色
            var command = new GetTokenRequestCommand();
            //command.Role = "admin";//登录按照用户的角色赋值
            return await this._mediator.Send(command);
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileContentResult> CreateValidateCode()
        {
            var key = base.HttpContext.Request.Query["validateKey"];
            FileContentResult contentResult = null;
            using (var stream = ValidateCodeHelper.Create(out string code))
            {
                var buffer = stream.ToArray();
                contentResult = File(buffer, "image/jpeg");
                this._cache.SetString($"ValidateKey_{key}", code, TimeSpan.FromMinutes(3), Consts.MainRedisKey);
            }
            return await Task.FromResult(contentResult);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<LoginResponseCommand> Login(LoginRequestCommand command)
        {
            return await this._mediator.Send(command);
        }
    }
}
