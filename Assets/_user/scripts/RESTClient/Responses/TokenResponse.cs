using System;
using RESTfull;

namespace RESTClient.Responses
{
    namespace Responses
    {
        [Serializable]
        public class TokenResponse : BaseRequestResponse
        {
            public string token;
        }
    }
}