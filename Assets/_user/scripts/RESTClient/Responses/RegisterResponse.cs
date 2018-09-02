using System;
using Newtonsoft.Json;
using RESTfull;

namespace RESTClient.Responses
{
    [Serializable]
    public class RegisterResponse : BaseRequestResponse
    {
        [JsonProperty("id")] public string Id;
        [JsonProperty("full_name")] public string FullName { get; set; }
        [JsonProperty("nickname")] public string Nickname { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
    }
}