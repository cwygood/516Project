using Domain.Interfaces;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Polly
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _facotry;
        public HttpMessageHandler MessageHandler { get; set; }
        private readonly IOptionsMonitor<HttpClientConfiguration> _options;

        public HttpService(IHttpClientFactory factory, IOptionsMonitor<HttpClientConfiguration> options)
        {
            this._facotry = factory;
            this._options = options;
        }

        private HttpClient GetClient()
        {
            if (this.MessageHandler != null)
            {
                return new HttpClient(this.MessageHandler);
            }
            
            return this._facotry.CreateClient(this._options.CurrentValue.PollyName);
        }

        public async Task<T> GetAsync<T>(string url) where T : class
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var res = await this.GetClient().SendAsync(requestMessage);
            string httpJsonString = res.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(httpJsonString);
        }

        public async Task<T> PostAsync<T>(string url, Dictionary<string,object> paras) where T : class
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(paras), Encoding.UTF8, "application/json");
            var res = await this.GetClient().SendAsync(requestMessage);
            string httpJsonString = res.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(httpJsonString);
        }

        public async Task<string> GetAsync(string url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var res = await this.GetClient().SendAsync(requestMessage);
            string httpJsonString = res.Content.ReadAsStringAsync().Result;

            return httpJsonString;
        }
    }
}
