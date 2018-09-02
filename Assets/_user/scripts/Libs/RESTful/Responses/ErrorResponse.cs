using System;
using Newtonsoft.Json;
using RESTfull;

namespace RESTfull.Responses
{
    [Serializable]
    public class ErrorResponse : BaseRequestResponse
    {
        [JsonProperty("code")] public int Code;
        [JsonProperty("message")] public string Message;
    }
}