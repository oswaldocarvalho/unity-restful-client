using System;
using RESTfull;

namespace RESTClient.Requests
{
    [Serializable]
    public class RegisterRequest : BaseRequestResponse
    {
        public string fullName;
        public string nickname;
        public string email;
        public string password;
    }
}