using System;
using RESTfull;

namespace RESTClient.Requests
{
    [Serializable]
    public class RegisterRequest : BaseRequestResponse
    {
        public string fullName { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public string password;
    }
}