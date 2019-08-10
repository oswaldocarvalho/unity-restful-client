using System;

namespace RESTfull.Responses
{
    [Serializable]
    public class ErrorResponse : BaseRequestResponse
    {
        public int code;
        public string message;
    }
}