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
        const string apiUrl = "http://localhost:5000/textprocessor/";
        public static HttpClient httpClient = new HttpClient();
        public static async Task<List<string>> GetOrderOptions(HttpClient client)
        {
            HttpResponseMessage response = null;
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:5000/textprocessor/GetOrderOptions"));
            try
            {
                response = await client.SendAsync(requestMessage);
                return await response.Content.ReadFromJsonAsync<List<string>>();

            }
            catch (Exception e)
            {
                
                
            }
            return null;

        }

        public static async Task<string> GetStatistics(HttpClient client, string text)
        {
            var paramethers = new Dictionary<string, string>()
            {
                {"textToAnalyze", text}
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(QueryHelpers.AddQueryString("http://localhost:5000/textprocessor/GetStatistics", paramethers)));

            var responseMessage = await client.SendAsync(requestMessage);

            return await responseMessage.Content.ReadAsStringAsync();
            
        }

        public static async Task<string> GetOrderedText(HttpClient client, string text, string orderOption)
        {
            var paramethers = new Dictionary<string, string>()
            {
                {"textToOrder", text},
                {"orderOption", orderOption}
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, 
                new Uri(QueryHelpers.AddQueryString("http://localhost:5000/textprocessor/GetOrderedText", paramethers)));

            var responseMessage = await client.SendAsync(requestMessage);

            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
