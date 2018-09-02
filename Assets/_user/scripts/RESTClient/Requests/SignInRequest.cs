using System;
using RESTfull;
using Newtonsoft.Json;

namespace RESTClient.Requests
{
    [Serializable]
    public class SignInRequest : BaseRequestResponse
    {
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("password")] public string Password;
    }
}