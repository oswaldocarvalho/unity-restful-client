using System;
using RESTfull;

namespace RESTClient.Responses
{
    [Serializable]
    public class SignInResponse : BaseRequestResponse
    {
        public int id;
        public string fullName;
        public string nickname;
        public string token;
    }
}