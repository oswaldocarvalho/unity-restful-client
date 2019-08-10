using System;
using RESTfull;

namespace RESTClient.Requests
{
    [Serializable]
    public class SignInRequest : BaseRequestResponse
    {
        public string email;
        public string password;
    }
}