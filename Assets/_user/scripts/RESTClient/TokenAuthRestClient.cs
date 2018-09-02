using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTClient.Responses.Responses;
using RESTfull;

namespace RESTClient
{
    public class TokenAuthRestClient : RestClient
    {
        private string _token;

        private string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                AddHeader("token", value);
            }
        }

        public TokenAuthRestClient(string baseUrl, string token = null) : base(baseUrl)
        {
            Token = token;
        }

        protected override async Task ProcessResponse(HttpResponseMessage response)
        {
            await base.ProcessResponse(response);

            TokenResponse tokenResponse =
                JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());

            if (tokenResponse != null && tokenResponse.Token != null)
            {
                Token = tokenResponse.Token;
            }
        }
    }
}