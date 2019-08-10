using System.Net.Http;
using System.Threading.Tasks;
using RESTClient.Responses.Responses;
using RESTfull;
using UnityEngine;

namespace RESTClient
{
    public class TokenAuthRestClient : RestClient
    {
        private string token;

        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
                AddHeader("token", value);
            }
        }

        public TokenAuthRestClient(string baseUrl, string token = null) : base(baseUrl)
        {
            this.token = token;
        }

        protected override async Task ProcessResponse(HttpResponseMessage response)
        {
            await base.ProcessResponse(response);

            TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(await response.Content.ReadAsStringAsync());

            if (tokenResponse != null && tokenResponse.token != null)
            {
                this.Token = tokenResponse.token;
            }
        }
    }
}