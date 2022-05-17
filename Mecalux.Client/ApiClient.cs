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
        const string apiUrl = "http://localhost:5000/textprocessor";
        public static HttpClient httpClient = new HttpClient();


        public async Task<List<string>> GetOrderOptions()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri($"{apiUrl}/GetOrderOptions"));
            
            try
            {
                HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
                return await response.Content.ReadFromJsonAsync<List<string>>();

            }
            catch (Exception e)
            {
                
            }
            return null;

        }

        public async Task<string> GetStatistics(string text)
        {
            var paramethers = new Dictionary<string, string>()
            {
                {"textToAnalyze", text}
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(QueryHelpers.AddQueryString($"{apiUrl}/GetStatistics", paramethers)));
            try
            {
                var responseMessage = await httpClient.SendAsync(requestMessage);
                return await responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

            }

            return null;
        }

        public async Task<string> GetOrderedText(string text, string orderOption)
        {
            var paramethers = new Dictionary<string, string>()
            {
                {"textToOrder", text},
                {"orderOption", orderOption}
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, 
                new Uri(QueryHelpers.AddQueryString($"{apiUrl}/GetOrderedText", paramethers)));
            try
            {
                var responseMessage = await httpClient.SendAsync(requestMessage);
                return await responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

            }

            return null;
        }
    }
}
