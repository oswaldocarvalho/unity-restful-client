using System;
using Newtonsoft.Json;
using RESTfull;

namespace RESTClient.Responses
{
    namespace Responses
    {
        [Serializable]
        public class TokenResponse : BaseRequestResponse
        {
            [JsonProperty("token")] public string Token;
        }
    }
}