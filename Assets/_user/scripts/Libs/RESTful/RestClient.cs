using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RESTfull.Responses;
using UnityEngine;

namespace RESTfull
{
    public class RestClient
    {
        private readonly Uri _baseUri;
        private readonly HttpClient _client;

        public RestClient(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
            _client = new HttpClient {BaseAddress = _baseUri};

            //
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            AddHeader("User-Agent", "UnityHttpClient/1.0");
        }

        public void AddHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Remove(key);
            _client.DefaultRequestHeaders.Add(key, value);
        }

        public async Task<T> Get<T>(string route, BaseRequestResponse baseRequest=null) where T : BaseRequestResponse
        {
            string query="";

            if (baseRequest != null)
            {
                // FIXME: find a better way to make this
                JObject jObj = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(baseRequest));
                query = "?" + String.Join("&", jObj.Children().Cast<JProperty>().Select(jp=>jp.Name + "=" + WWW.EscapeURL(jp.Value.ToString())));
                Debug.Log(query);
            }
            
            HttpResponseMessage response = await _client.GetAsync($"{route}{query}");
            
            await ProcessResponse(response);
            
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Post<T>(string route, BaseRequestResponse baseRequest) where T : BaseRequestResponse
        {
            StringContent jsonRequest = new StringContent(JsonConvert.SerializeObject(baseRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(route, jsonRequest);

            await ProcessResponse(response);
            
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Put<T>(string route, BaseRequestResponse baseRequest) where T : BaseRequestResponse
        {
            StringContent jsonRequest = new StringContent(JsonConvert.SerializeObject(baseRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(route, jsonRequest);

            await ProcessResponse(response);

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Patch<T>(string route, BaseRequestResponse baseRequest) where T : BaseRequestResponse
        {
           
            HttpMethod method = new HttpMethod("PATCH");
            StringContent content = new StringContent(JsonConvert.SerializeObject(baseRequest), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(method, new Uri(_baseUri, route))
            {
                Content = content
            };
            HttpResponseMessage response = await _client.SendAsync(httpRequest);

            await ProcessResponse(response);

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Delete<T>(string route) where T : BaseRequestResponse
        {
            HttpResponseMessage response = await _client.DeleteAsync(new Uri(_baseUri, route));

            await ProcessResponse(response);

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        protected virtual async Task ProcessResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ResponseException(JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync()));
            }
        }
    }
}
