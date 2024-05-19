using Quiz.Contracts.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Quiz.Contracts.Services
{
    public class HttpFormService : IHttpFormService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpFormService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Submit(string jsonObject, string url, string token)
        {
            var httpClient = _httpClientFactory.CreateClient("HttpForm");

            httpClient.DefaultRequestHeaders.Add("Authorization", token);

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            else
            {
                return "";
            }


        }
    }
}
