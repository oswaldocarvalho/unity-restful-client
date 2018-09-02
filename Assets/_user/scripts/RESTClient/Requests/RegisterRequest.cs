using System;
using RESTfull;
using Newtonsoft.Json;

namespace RESTClient.Requests
{
    [Serializable]
    public class RegisterRequest : BaseRequestResponse
    {
        [JsonProperty("full_name")] public string FullName { get; set; }
        [JsonProperty("nickname")] public string Nickname { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("password")] public string Password;
    }
}