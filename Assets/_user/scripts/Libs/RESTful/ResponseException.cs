using System;
using RESTfull.Responses;

namespace RESTfull
{
    [Serializable]
    public class ResponseException : Exception
    {
        public ResponseException() : base() {}
        public ResponseException(ErrorResponse errorResponse) : base(errorResponse.message) {}
    }
}
