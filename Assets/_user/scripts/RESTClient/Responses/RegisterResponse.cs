using System;
using RESTfull;

namespace RESTClient.Responses
{
    [Serializable]
    public class RegisterResponse : BaseRequestResponse
    {
        public string id;
        public string fullName;
        public string Nickname;
        public string email;
    }
}