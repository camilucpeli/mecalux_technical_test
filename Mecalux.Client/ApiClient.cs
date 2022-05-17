using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mecalux.Client
{
    public class ApiClient
    {
        private static HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<List<string>> GetOrderOptions()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:14930/textprocessor/GetOrderOptions"));

            var responseMessage = await _client.SendAsync(requestMessage);

            return await responseMessage.Content.ReadFromJsonAsync<List<string>>();
        }

        public async Task<string> GetStatistics(string text)
        {
            var paramethers = new Dictionary<string, string>()
            {
                {"textToAnalyze", text}
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(QueryHelpers.AddQueryString("http://localhost:14930/textprocessor/GetStatistics", paramethers)));

            var responseMessage = await _client.SendAsync(requestMessage);

            return await responseMessage.Content.ReadAsStringAsync();
            
        }

        public async Task<string> GetOrderedText(string text, string orderOption)
        {
            var paramethers = new Dictionary<string, string>()
            {
                {"textToOrder", text},
                {"orderOption", orderOption}
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, 
                new Uri(QueryHelpers.AddQueryString("http://localhost:14930/textprocessor/GetOrderedText", paramethers)));

            var responseMessage = await _client.SendAsync(requestMessage);

            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
