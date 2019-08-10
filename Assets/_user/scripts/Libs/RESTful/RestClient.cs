using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
                query = baseRequest.GetQueryString();
                Debug.Log(query);
            }
            
            HttpResponseMessage response = await _client.GetAsync($"{route}{query}");
            
            await ProcessResponse(response);

            return JsonUtility.FromJson<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Post<T>(string route, BaseRequestResponse baseRequest) where T : BaseRequestResponse
        {
			StringContent jsonRequest = new StringContent(JsonUtility.ToJson(baseRequest), Encoding.UTF8, "application/json");
            Debug.Log(jsonRequest);
            HttpResponseMessage response = await _client.PostAsync(route, jsonRequest);

            await ProcessResponse(response);

			return JsonUtility.FromJson<T>(await response.Content.ReadAsStringAsync());
		}

        public async Task<T> Put<T>(string route, BaseRequestResponse baseRequest) where T : BaseRequestResponse
        {
            StringContent jsonRequest = new StringContent(JsonUtility.ToJson(baseRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(route, jsonRequest);

            await ProcessResponse(response);

			return JsonUtility.FromJson<T>(await response.Content.ReadAsStringAsync());
		}

        public async Task<T> Patch<T>(string route, BaseRequestResponse baseRequest) where T : BaseRequestResponse
        {
           
            HttpMethod method = new HttpMethod("PATCH");
            StringContent content = new StringContent(JsonUtility.ToJson(baseRequest), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(method, new Uri(_baseUri, route))
            {
                Content = content
            };
            HttpResponseMessage response = await _client.SendAsync(httpRequest);

            await ProcessResponse(response);

            return JsonUtility.FromJson<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Delete<T>(string route) where T : BaseRequestResponse
        {
            HttpResponseMessage response = await _client.DeleteAsync(new Uri(_baseUri, route));

            await ProcessResponse(response);

            return JsonUtility.FromJson<T>(await response.Content.ReadAsStringAsync());
        }

        protected virtual async Task ProcessResponse(HttpResponseMessage response)
        {
            Debug.Log("STATUS CODE : " + response.StatusCode);
            if (!response.IsSuccessStatusCode)
            {
				string jsonResponse = await response.Content.ReadAsStringAsync();

				Debug.Log(jsonResponse);

				throw new ResponseException(JsonUtility.FromJson<ErrorResponse>(jsonResponse));
            }
        }
    }
}
