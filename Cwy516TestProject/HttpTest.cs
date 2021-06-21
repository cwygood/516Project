using RichardSzalay.MockHttp;
using System;
using Xunit;
using System.Net.Http;
using Domain.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Application.Commands.HomeCommands;

namespace Cwy516TestProject
{
    public class HttpTest : IClassFixture<CustomTestFixture>
    {
        private CustomTestFixture _factory;
        public HttpTest(CustomTestFixture factory)
        {
            this._factory = factory;
        }
        [Fact]
        public async void Test1()
        {
            MockHttpMessageHandler mockHandler = null; //new MockHttpMessageHandler();    //必须保证地址可以访问
            //mockHandler.When("http://localhost:5100/api/Home/Index").Respond("application/json", JsonConvert.SerializeObject(new { id="1111",name="222" }));//mock数据
            //mockHandler.When("http://localhost:5100/api/Home/Index").Throw(new TimeoutException("错误"));//测试polly
            this._factory.handler = mockHandler;

            var client = this._factory.CreateClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/Home/GetAllUser");//当前项目的路由地址（忽略ip+port）
            //requestMessage.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "RequestId", "1234" } }), Encoding.UTF8, "application/json");

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(new GetAllUserRequestCommand() { RequestId="6666" }), Encoding.UTF8, "application/json");

            var res = await client.SendAsync(requestMessage);
            var respond = JsonConvert.DeserializeObject<GetAllUserResponseCommand>(res.Content.ReadAsStringAsync().Result);
            Assert.True(respond.IsSuccess);
        }
    }
}
